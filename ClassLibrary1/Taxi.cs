namespace ClassLibrary1
{
    public class Taxi
    {

        public static int taxiCount = 0;
        public int Id { get; set; }

        public bool Booked { get; set; }

        public char CurrentSpot { get; set; } //where taxi is now

        public int FreeTime { get; set; } //when taxi becomes free

        public int TotalEarnings { get; set; } //total earnings of taxi

        public List<string> trips;


        public Taxi()
        {
            this.Booked = false;
            this.CurrentSpot = 'A';
            this.FreeTime = 6; //like 6am
            this.TotalEarnings = 0;
            trips = new List<string>();
            taxiCount = taxiCount + 1;
            this.Id = taxiCount;
        }

        public void SetDetails(bool booked, char currentSpot, int freeTime, int totalEarnings, string tripDetail)
        {
            this.Booked = booked;
            this.CurrentSpot = currentSpot;
            this.FreeTime = freeTime;
            this.TotalEarnings = totalEarnings;
            this.trips.Add(tripDetail);
        }

        public void PrintDetails()
        {
            //print all trips details
            Console.WriteLine("Taxi - " + this.Id + " Total Earnings - " + this.TotalEarnings);
            Console.WriteLine("TaxiID    BookingID    CustomerID    From    To    PickupTime    DropTime    Amount");
            foreach (string trip in trips)
            {
                Console.WriteLine(Id + "          " + trip);
            }
            Console.WriteLine("--------------------------------------------------------------------------------------");
        }

        public void PrintTaxiDetails()
        {
            //print total earningand taxi details like current location and free time
            Console.WriteLine("Taxi - " + this.Id + " Total Earnings - " + this.TotalEarnings + " Current spot - " + this.CurrentSpot + " Free Time - " + this.FreeTime);
        }

    }
}
