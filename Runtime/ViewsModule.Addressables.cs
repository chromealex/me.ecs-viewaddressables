#if ENABLE_IL2CPP
#define INLINE_METHODS
#endif

#if VIEWS_MODULE_SUPPORT
namespace ME.ECS {
    
    using Views;
    using ME.ECS.Views.Providers;

    public static class ViewsModuleAddressables {

        #if INLINE_METHODS
        [System.Runtime.CompilerServices.MethodImplAttribute(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        public static ViewId RegisterViewSource<T>(this ViewsModule viewsModule, UnityEngine.AddressableAssets.AssetReferenceT<T> addressablePrefab, ViewId customId = default) where T : UnityEngine.Object {

            #if GAMEOBJECT_VIEWS_MODULE_SUPPORT
            if (typeof(MonoBehaviourViewBase).IsAssignableFrom(typeof(T))) {
                
                return viewsModule.RegisterViewSource<UnityGameObjectProviderInitializer>(addressablePrefab, customId);

            }
            #endif

            #if PARTICLES_VIEWS_MODULE_SUPPORT
            if (typeof(ParticleViewSourceBase).IsAssignableFrom(typeof(T))) {
                
                return viewsModule.RegisterViewSource<UnityParticlesProviderInitializer>(addressablePrefab, customId);

            }
            #endif

            #if DRAWMESH_VIEWS_MODULE_SUPPORT
            if (typeof(DrawMeshViewSourceBase).IsAssignableFrom(typeof(T))) {
                
                return viewsModule.RegisterViewSource<UnityDrawMeshProviderInitializer>(addressablePrefab, customId);

            }
            #endif

            if (typeof(NoViewBase).IsAssignableFrom(typeof(T))) {
                
                return viewsModule.RegisterViewSource<NoViewProviderInitializer>(addressablePrefab, customId);

            }

            ViewProviderNotFound.Throw(typeof(T));
            return default;

        }

        #if INLINE_METHODS
        [System.Runtime.CompilerServices.MethodImplAttribute(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        public static ViewId RegisterViewSource<TProvider>(this ViewsModule viewsModule, UnityEngine.AddressableAssets.AssetReference addressablePrefab, ViewId customId = default) where TProvider : struct, IViewsProviderInitializer {

            var providerInitializer = new TProvider();
            
            if (addressablePrefab == null) {

                ViewSourceIsNullException.Throw();
                return default;

            }

            #if VIEWS_REGISTER_VIEW_SOURCE_CHECK_STATE
            E.IS_NOT_LOGIC_STEP(viewsModule.world);
            #endif

            var handle = addressablePrefab.LoadAssetAsync<IView>();
            return viewsModule.RegisterViewSource(providerInitializer, ViewsModule.ViewSourceObject.Create(handle), customId);

        }

    }

}
#endif