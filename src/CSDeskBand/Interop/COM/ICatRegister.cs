using System;
using System.Runtime.InteropServices;

namespace CSDeskBand.Interop.COM
{
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("0002E012-0000-0000-C000-000000000046")]
    internal interface ICatRegister
    {
        [PreserveSig]
        void RegisterCategories(uint cCategories, [MarshalAs(UnmanagedType.LPArray)] CATEGORYINFO[] rgCategoryInfo);

        [PreserveSig]
        void RegisterClassImplCategories([In] ref Guid rclsid, uint cCategories, [MarshalAs(UnmanagedType.LPArray)] Guid[] rgcatid);

        [PreserveSig]
        void RegisterClassReqCategories([In] ref Guid rclsid, uint cCategories, [MarshalAs(UnmanagedType.LPArray)] Guid[] rgcatid);

        [PreserveSig]
        void UnRegisterCategories(uint cCategories, [MarshalAs(UnmanagedType.LPArray)] Guid[] rgcatid);

        [PreserveSig]
        void UnRegisterClassImplCategories([In] ref Guid rclsid, uint cCategories, [MarshalAs(UnmanagedType.LPArray)] Guid[] rgcatid);

        [PreserveSig]
        void UnRegisterClassReqCategories([In] ref Guid rclsid, uint cCategories, [MarshalAs(UnmanagedType.LPArray)] Guid[] rgcatid);
    }

    //Exceptions when using this. Seems that dlls need to be registered in GAC
    internal class ComponentCategoryManager
    {
        public static readonly Guid CATID_DESKBAND = new Guid("00021492-0000-0000-C000-000000000046");

        private static readonly Guid _componentCategoryManager = new Guid("0002e005-0000-0000-c000-000000000046");
        private static readonly ICatRegister _catRegister;
        private Guid _classId;

        static ComponentCategoryManager()
        {
            _catRegister = Activator.CreateInstance(Type.GetTypeFromCLSID(_componentCategoryManager, true)) as ICatRegister;
        }

        private ComponentCategoryManager(Guid classId)
        {
            _classId = classId;
        }

        public static ComponentCategoryManager For(Guid classId)
        {
            return new ComponentCategoryManager(classId);
        }

        public void RegisterCategories( Guid[] categoryIds)
        {
            _catRegister.RegisterClassImplCategories(ref _classId, (uint)categoryIds.Length, categoryIds);
        }

        public void UnRegisterCategories(Guid[] categoryIds)
        {
            _catRegister.UnRegisterClassImplCategories(ref _classId, (uint)categoryIds.Length, categoryIds);
        }
    }
}
