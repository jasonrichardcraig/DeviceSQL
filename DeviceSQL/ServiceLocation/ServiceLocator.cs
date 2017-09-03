#region Imported Types

using System;

#endregion

namespace DeviceSQL.ServiceLocation
{
	/// <summary>
	/// This class provides the ambient container for this application. If your
	/// framework defines such an ambient container, use ServiceLocator.Current
	/// to get it.
	/// </summary>
	public static class ServiceLocator
	{
        private const string SERVICE_LOCATION_PROVIDER_NOT_SET_MESSAGE = "ServiceLocationProvider must be set.";
		private static ServiceLocatorProvider currentProvider;

		/// <summary>
		/// The current ambient container.
		/// </summary>
		public static IServiceLocator Current
		{
			get
			{
				if (!ServiceLocator.IsLocationProviderSet)
				{
                    throw new InvalidOperationException(SERVICE_LOCATION_PROVIDER_NOT_SET_MESSAGE);
				}
				return ServiceLocator.currentProvider();
			}
		}

		public static bool IsLocationProviderSet
		{
			get
			{
				return ServiceLocator.currentProvider != null;
			}
		}

		/// <summary>
		/// Set the delegate that is used to retrieve the current container.
		/// </summary>
		/// <param name="newProvider">Delegate that, when called, will return
		/// the current ambient container.</param>
		public static void SetLocatorProvider(ServiceLocatorProvider newProvider)
		{
			ServiceLocator.currentProvider = newProvider;
		}
	}
}