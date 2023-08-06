namespace Schema
{
    public class UserResponse
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string PlateNo { get; set; }

        public virtual List<BankCardInfoResponse> BankCards { get; set; }
        public virtual List<ApartmentResponse> Apartments { get; set; }
    }
}
