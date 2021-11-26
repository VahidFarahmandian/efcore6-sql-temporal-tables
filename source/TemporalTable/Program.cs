
SampleDbContext ctx = new();

foreach (var item in ctx.Products.ToList())
{
    Console.WriteLine(item);
}

Console.WriteLine("*********TemporalAll************");
foreach (var item in
    ctx.Products
    .TemporalAll()
    .Select(x => new
    {
        x.Name,
        x.Price,
        ValidFrom = EF.Property<DateTime>(x, "PeriodStart"),
        ValidTo = EF.Property<DateTime>(x, "PeriodEnd")
    })
    )
{
    Console.WriteLine($"{item},From:{item.ValidFrom}, To:{item.ValidTo} ");
}

Console.WriteLine("********TemporalBetween************");
foreach (var item in ctx.Products
    .TemporalBetween(DateTime.MinValue, new DateTime(2021, 11, 22, 16, 00, 00))
    .Select(x => new
    {
        x.Name,
        x.Price,
        ValidFrom = EF.Property<DateTime>(x, "PeriodStart"),
        ValidTo = EF.Property<DateTime>(x, "PeriodEnd")
    }))
{
    Console.WriteLine($"{item},From:{item.ValidFrom}, To:{item.ValidTo} ");
}

Console.WriteLine("********TemporalAsOf*************");
foreach (var item in ctx.Products
    .TemporalAsOf(new DateTime(2021, 11, 22, 16, 00, 00)).Select(x => new { x.Name, x.Price, ValidFrom = EF.Property<DateTime>(x, "PeriodStart"), ValidTo = EF.Property<DateTime>(x, "PeriodEnd") }))
{
    Console.WriteLine($"{item},From:{item.ValidFrom}, To:{item.ValidTo} ");
}

Console.WriteLine("********Restore Deleted Data************");
var latestModification = ctx.Products
    .TemporalAll()
    .Where(x => x.Name == "Prod1")
    .OrderBy(x => EF.Property<DateTime>(x, "PeriodEnd"))
    .Select(x => EF.Property<DateTime>(x, "PeriodStart"))
    .Last();

var restoredVersion = ctx.Products
    .TemporalAsOf(latestModification.AddMilliseconds(-1))
    .Single(x => x.Name == "Prod1");
Console.WriteLine(restoredVersion);