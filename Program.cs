class Orszag
{
    public string Nev { get; private set; }
    public int Terulet { get; private set; }
    public int Nepesseg { get; private set; }
    public string Fovaros { get; private set; }
    public int FovarosLakossag { get; private set; }

    public int Nepsuruseg => Nepesseg / Terulet;
    public bool FovarosLakossag30 => FovarosLakossag * 1000.0 / Nepesseg > 0.3;

    public Orszag(string adatsor)
    {
        var adatok = adatsor.Split(';');
        Nev = adatok[0];
        Terulet = int.Parse(adatok[1]);
        Nepesseg = adatok[2].Contains('g') ? int.Parse(adatok[2].Replace("g", "")) * 10000 : int.Parse(adatok[2]);
        Fovaros = adatok[3];
        FovarosLakossag = int.Parse(adatok[4]);
    }

    public override string ToString() => $"{Nev} ({Fovaros})";
}

class Nepesseg
{
    static void Main()
    {
        var orszagok = File.ReadLines("adatok-utf8.txt").Skip(1).Select(sor => new Orszag(sor)).ToList();

        Console.WriteLine($"4. feladat\nOrszágok száma: {orszagok.Count}.\n");

        var kina = orszagok.First(x => x.Nev == "Kína");
        Console.WriteLine($"5. feladat\nKína népsűrűsége: {kina.Nepsuruseg} fő/km²\n");

        var india = orszagok.First(x => x.Nev == "India");
        Console.WriteLine($"6. feladat\nKína lakossága {kina.Nepesseg - india.Nepesseg} fővel több Indiánál.\n");

        var harmadik = orszagok.Where(x => x.Nev != "Kína" && x.Nev != "India").OrderByDescending(x => x.Nepesseg).First();
        Console.WriteLine($"7. feladat\nA harmadik legnépesebb ország: {harmadik.Nev}, lakossága {harmadik.Nepesseg} fő.\n");

        Console.WriteLine("8. feladat - Országok, ahol a lakosság több mint 30%-a a fővárosban él:");
        orszagok.Where(x => x.FovarosLakossag30).ToList().ForEach(Console.WriteLine);
    }
}
