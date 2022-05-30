
using Searchfight.Infrastructure.Factorys;
    
    try
    {
        if ( args.Length == 0 )
        {
            Console.WriteLine("Please enter a query to search....");
            args = Console.ReadLine()?.Split(' ');
        }

        if (  args !=null && args.Length > 0 )
        {
            Console.WriteLine("Loading results ...");
            var searchManager = SearchFightFactory.CreateSearchManager();
            var result = await searchManager.GetSearchReport(args.ToList());
            Console.WriteLine(result);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Unexpected error generating the report: {ex.Message}");
    }
    Console.ReadKey();

