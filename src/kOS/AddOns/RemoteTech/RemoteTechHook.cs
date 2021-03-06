﻿using System;
using System.Linq;
using kOS.Safe.Utilities;
using kOS.Suffixed;

namespace kOS.AddOns.RemoteTech
{
    public static class RemoteTechHook
    {
        private const String REMOTE_TECH_ASSEMBLY = "RemoteTech";
        private const String REMOTE_TECH_API = "RemoteTech.API.API";
        private const String ALT_REMOTE_TECH_API = "RemoteTech.API";

        private static bool hookFail;
        private static IRemoteTechAPIv1 instance;
        public static IRemoteTechAPIv1 Instance
        {
            get
            {
                if (hookFail) return null;
                instance = instance ?? InitializeAPI();
                if (instance == null) hookFail = true;
                return instance;
            }
        }

        public static bool IsAvailable(Guid vesselId)
        {
            try
            {
                var isAvailableBase = IsAvailable();
                if (!isAvailableBase)
                {
                    return false;
                }
                var hasFlightComputer = Instance.HasFlightComputer(vesselId);
                return hasFlightComputer;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsAvailable()
        {
            // reading the config is the slowest of the 3 checks, so do it last.
            return Instance != null && Instance.IsRemoteTechEnabled() && SafeHouse.Config.EnableRTIntegration;
        }

        private static IRemoteTechAPIv1 InitializeAPI()
        {  
            SafeHouse.Logger.Log(string.Format("Looking for RemoteTech")); 
            var loadedAssembly = AssemblyLoader.loadedAssemblies.FirstOrDefault(a => a.assembly.GetName().Name.Equals(REMOTE_TECH_ASSEMBLY));
            if (loadedAssembly == null) return null;
            SafeHouse.Logger.Log(string.Format("Found RemoteTech! Version: {0}.{1}", loadedAssembly.versionMajor, loadedAssembly.versionMinor)); 

            var type = loadedAssembly.assembly.GetTypes().FirstOrDefault(t => t.FullName.Equals(REMOTE_TECH_API)) ??
                       loadedAssembly.assembly.GetTypes().FirstOrDefault(t => t.FullName.Equals(ALT_REMOTE_TECH_API));

            if (type == null) return null;

            SafeHouse.Logger.Log(string.Format("Found API! {0} ", type.Name)); 
            var methods = type.GetMethods();
            var api = new RemoteTechAPI();

            try
            {
                foreach (var property in api.GetType().GetProperties())
                {
                    var method = methods.FirstOrDefault(m =>
                    {
                        if (m.Name.Equals(property.Name))
                        {
                            SafeHouse.Logger.Log(string.Format("Found Endpoint: {0}", m.Name)); 
                            return true;
                        }
                        return false;
                    });

                    if (method == null)
                    {
                        throw new ArgumentNullException(property.Name);
                    }

                    var del = Delegate.CreateDelegate(property.PropertyType, type, method.Name);
                    property.SetValue(api, del, null);
                }
            }
            catch (Exception e)
            {
                SafeHouse.Logger.Log("Error creating RemoteTech interface: " + e); 
                return null;
            }

            SafeHouse.Logger.Log("RemoteTech interface successfully created."); 
            return api;
        }
    }
}
