using TravelDeskManagement.Models;

namespace TravelDeskManagement.Helpers
{
    public static class Extensions
    {
        public static string GetPurposeDisplay(this TravelRequest request)
        {
            return request.Purpose == TravelPurpose.Other
                ? request.OtherPurpose
                : request.Purpose.ToString();
        }
    }
}
