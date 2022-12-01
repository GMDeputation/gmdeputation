using System;

namespace SFA.Models
{
	public class AccomodationBooking
	{
		public int Id { get; set; }

		public int RequestedUserId { get; set; }

		public string UserName { get; set; }

		public int DistrictId { get; set; }

		public string DistrictName { get; set; }

		public int ChurchId { get; set; }

		public string ChurchName { get; set; }

		public int AccomodationId { get; set; }

		public string AccomodationDesc { get; set; }

		public int? AdultNo { get; set; }

		public int? ChildNo { get; set; }

		public DateTime CheckinDate { get; set; }

		public DateTime CheckoutDate { get; set; }

		public TimeSpan? ArrivalTime { get; set; }

		public TimeSpan? DepartureTime { get; set; }

		public string Reason { get; set; }

		public string FeedBack { get; set; }

		public bool IsSubmit { get; set; }

		public bool IsApproved { get; set; }

		public int? ApprovedBy { get; set; }

		public DateTime? ApprovedOn { get; set; }

		public string Remarks { get; set; }

		public int CreatedBy { get; set; }

		public DateTime CreatedOn { get; set; }

		public int? ModifiedBy { get; set; }

		public DateTime? ModifiedOn { get; set; }

		public bool IsFeedBack { get; set; }

		public string AccessCode { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }


	}

	public class AccomodationBookingQuery : Query
	{
		public string SearchText { get; set; }

		public DateTime? FromDate { get; set; }

		public DateTime? ToDate { get; set; }
	}

}