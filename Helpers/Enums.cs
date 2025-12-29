namespace TravelDeskManagement.Helpers
{
    public enum Department
    {
        HR = 1,
        Management = 2,
        TeamLead = 3,
        ProjectManager = 4,
        Accountant = 5,
        Developers = 6,
        QA = 7,
        Wordpress = 8,
        UI = 9,
        CRM = 10,
    }

    public enum TravelPurpose
    {
        ClientVisit = 1,
        Conference = 2,
        Training = 3,
        InternalMeeting = 4,
        OnsiteSupport = 5,
        Other = 99
    }

    public enum TravelRequestStatus
    {
        Pending = 1,
        Approved = 2,
        Rejected = 3
    }
}
