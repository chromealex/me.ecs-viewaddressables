#if ENABLE_IL2CPP
#define INLINE_METHODS
#endif

#if VIEWS_MODULE_SUPPORT && GAMEOBJECT_VIEWS_MODULE_SUPPORT
namespace ME.ECS {
    
    using Views;
    using Views.Providers;

    public static class WorldAddressables {
        
        #if INLINE_METHODS
        [System.Runtime.CompilerServices.MethodImplAttribute(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        public static ViewId RegisterViewSource<T>(this World world, UnityEngine.AddressableAssets.AssetReferenceT<T> addressablePrefab, ViewId customId = default) where T : UnityEngine.Object {

            var viewsModule = world.GetModule<ViewsModule>();
            return viewsModule.RegisterViewSource(addressablePrefab, customId);

        }

        #if INLINE_METHODS
        [System.Runtime.CompilerServices.MethodImplAttribute(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        public static ViewId RegisterViewSource<TProvider>(this World world, UnityEngine.AddressableAssets.AssetReference addressablePrefab, ViewId customId = default)
            where TProvider : struct, IViewsProviderInitializer {
            
            var viewsModule = world.GetModule<ViewsModule>();
            return viewsModule.RegisterViewSource<TProvider>(addressablePrefab, customId);

        }
        
    }

}
#endif