using System.Net;
using CoreFoundation;
using Plugin.Connectivity;
using SystemConfiguration;

namespace SimpleFeedReader
{
    public class NetworkStatus
    {
        private static NetworkReachability defaultRouteReachability;

        public static bool IsDataConnectionAvailable()
        {
            return CrossConnectivity.Current.IsConnected;
        }

        private static bool IsNetworkAvailable(out NetworkReachabilityFlags flags)
        {
            if (defaultRouteReachability == null)
            {
                var ipAddress = new IPAddress(0);
                defaultRouteReachability = new NetworkReachability(ipAddress.MapToIPv6());
                defaultRouteReachability.Schedule(CFRunLoop.Current, CFRunLoop.ModeDefault);
            }
            return defaultRouteReachability.TryGetFlags(out flags);
        }

    }
}
