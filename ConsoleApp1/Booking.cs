
using ClassLibrary1;


public class Booking
{
    public static void BookTaxi(int customerID, char pickupPoint, char dropPoint, int pickupTime, List<Taxi> freeTaxis)
    {
        //to find the nearest
        int min = 999;

        //distance between pickup and drop
        int distanceBetweenPickupAndDrop = 0;

        //this trip earning
        int earning = 0;

        //when taxi will be free next
        int nextfreeTime = 0;

        //where taxi will be after trip is over
        char nextSpot = 'Z';

        Taxi bookedTaxi = null;


        //all details of the trip as string
        string tripDetail = "";

        foreach (Taxi t in freeTaxis)
        {
            int distanceBetweenCustomerAndTaxi = Math.Abs((t.CurrentSpot - '0') - (pickupPoint - '0')) * 15;

            if (distanceBetweenCustomerAndTaxi < min)
            {
                bookedTaxi = t;

                distanceBetweenPickupAndDrop = Math.Abs((dropPoint - '0') - (pickupPoint - '0')) * 15;

                earning = 100 + (distanceBetweenPickupAndDrop - 5) * 10;

                //drop time calc
                int dropTime = pickupTime + (distanceBetweenPickupAndDrop / 15);

                nextfreeTime = dropTime;

                nextSpot = dropPoint;
                //creating trip detail
                tripDetail = customerID + "               " + pickupPoint + "      " + dropPoint + "       " + pickupTime + "          " + dropTime + "           " + earning;
                min = distanceBetweenCustomerAndTaxi;
            }
        }

        //setting corresponding details to allotted taxi
        bookedTaxi.SetDetails(true, nextSpot, nextfreeTime, bookedTaxi.TotalEarnings + earning, tripDetail);
        Console.WriteLine("Taxi " + bookedTaxi.Id + " booked");

    }
    public static List<Taxi> CreateTaxis(int n)
    {
        List<Taxi> taxis = new List<Taxi>();
        //create taxis
        for (int i = 0; i < n; i++)
        {
            Taxi t = new Taxi();
            taxis.Add(t);
        }
        return taxis;
    }

    public static List<Taxi> GetFreeTaxis(List<Taxi> taxis, int pickupTime, char pickupPoint)
    {
        List<Taxi> freeTaxis = new List<Taxi>();

        foreach (Taxi t in taxis)
        {
            //taxi should be free
            // taxi should have enought time to reach customer before pickup time
            if (t.FreeTime <= pickupTime && (Math.Abs((t.CurrentSpot - '0') - (pickupPoint - '0')) <= pickupTime - t.FreeTime))
                freeTaxis.Add(t);
        }
        return freeTaxis;
    }
    static void Main(string[] args)
    {
        //create 4 taxis
        List<Taxi> taxis = CreateTaxis(4);
        int id = 1;

        while (true)
        {
            Console.WriteLine("0 - > Book Taxi");
            Console.WriteLine("1 - > Print Taxi details");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 0:
                    {
                        //getting details from the customer
                        int customerId = id;
                        Console.WriteLine("Enter pickup point");
                        char pickupPoint = char.Parse(Console.ReadLine().Trim());
                        Console.WriteLine("Enter drop point");
                        char dropPoint = char.Parse(Console.ReadLine().Trim());
                        Console.WriteLine("Enter Pickup time");
                        int pickupTime = int.Parse(Console.ReadLine().TrimEnd());
                        //check if pickup and drop points are valid
                        if (pickupPoint < 'A' || dropPoint > 'F' || pickupPoint > 'F' || dropPoint < 'A')
                        {
                            Console.WriteLine("Valid pickup and drop points are A, B, C, D, E, F. Exiting...");
                            return;
                        }

                        //get all free taxis that can reach customer on or before pickup time
                        List<Taxi> freeTaxis = GetFreeTaxis(taxis, pickupTime, pickupPoint);

                        //no free taxi means we cannot allot, exit!
                        if (freeTaxis.Count == 0)
                        {
                            Console.WriteLine("No Taxi is available. Exiting");
                            return;
                        }

                        //sort taxis based on earnings
                        freeTaxis.Sort((a, b) => a.TotalEarnings - b.TotalEarnings);

                        //get free taxi nearest to us
                        BookTaxi(id, pickupPoint, dropPoint, pickupTime, freeTaxis);
                        id++;
                        break;
                    }
                case 1:
                    {
                        foreach (Taxi t in taxis)
                        {
                            t.PrintDetails();
                        }
                        foreach (Taxi t in taxis)
                        {
                            t.PrintTaxiDetails();
                        }
                        break;
                    }
                default:
                    Console.WriteLine("choose 0 or 1");
                    break;
            }
        }
    }
}