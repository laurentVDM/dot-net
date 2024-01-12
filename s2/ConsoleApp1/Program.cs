// See https://aka.ms/new-console-template for more information
using LINQDataContext;

Console.WriteLine("Hello, World!");

DataContext dc = new DataContext();

Student? jdepp = (from student in dc.Students
                  where student.Login == "jdepp"
                  select student).SingleOrDefault();

if (jdepp != null)
{
    Console.WriteLine(jdepp.Last_Name + jdepp.First_Name);
}


//Ex 2.2

//Ecrire une requête pour présenter, pour chaque étudiant,
//son nom complet (nom et prénom séparés par un espace),
//son id et sa date de naissance.
//L ’objectif pour cet exercice est de réaliser un maximum dans le query et non dans l’affichage.
var QueryResult = from s in dc.Students
                  select new { Last_Name = s.Last_Name, First_name = s.First_Name, id = s.Student_ID, BirthDate = s.BirthDate };

foreach(var s in QueryResult)
{
    Console.WriteLine("Nom:{0} {1}, Id:{2}, Date naissance:{3}",s.Last_Name,s.First_name, s.id, s.BirthDate);
}

//Ex 3.1

var QueryResult2 = from s in dc.Students
                    where s.BirthDate.Year < 1955
                    select new { Last_Name = s.Last_Name, Year_Result = s.Year_Result, status = (s.Year_Result<12)? "KO" : "OK" };

foreach(var s in QueryResult2)
{
    Console.WriteLine("Lastname:{0}, YearResult:{1}, Status: {2}", s.Last_Name, s.Year_Result, s.status);
}

//Ex 3.4
//Ecrire une requête pour présenter le nom et
//le résultat annuel classé par résultats annuels décroissants de tous les étudiants
//qui ont obtenu un résultat inférieur ou égal à 3.

IEnumerable<Student> QueryResult3 = from s in dc.Students
                   where s.Year_Result <= 3
                   orderby s.Year_Result descending
                   select s;
foreach(var s in QueryResult3)
{
    Console.WriteLine("nom: {0} {1}, resultat annuel:{2}", s.Last_Name, s.First_Name, s.Year_Result);
}

//Ex 3.5
//Ecrire une requête pour présenter le nom complet (nom et prénom séparés par un espace)
//et le résultat annuel classé par ordre croissant sur le nom des étudiants appartenant à la section 1110.

IEnumerable<Student> QueryResult4 = from s in dc.Students
                                    where s.Section_ID == 1110
                                    orderby s.Last_Name
                                    select s;

foreach(var s in QueryResult4)
{
    Console.WriteLine("nom: {0} {1}, résultat annuel: {2}", s.Last_Name, s.First_Name, s.Year_Result);
}

//Ex 4.1
// Donner le résultat annuel moyen pour l’ensemble des étudiants.
Console.WriteLine("la moyenne est de : {0}", dc.Students.Average(s => s.Year_Result));

//Ex 4.5
//Donner le nombre de lignes qui composent la « table » STUDENT.
Console.WriteLine("le nbr de lignes dans la table student : {0}", dc.Students.Count());

//Ex 5.1
//Donner pour chaque section, le résultat maximum (Max_Result) obtenu par les étudiants.
IEnumerable<IGrouping<int, Student>> QueryResult5 =
    dc.Students.GroupBy(s => s.Section_ID);
foreach(IGrouping<int,Student> g in QueryResult5)
{
    int maxResultInSection = g.Max(s => s.Year_Result);
    Console.WriteLine("section {0}, resultat max: {1}", g.Key, maxResultInSection);
}

//Ex 5.3
//Donner le résultat moyen (AVG_Result) et le mois en chiffre (BirtMonth)
//pour les étudiants né le même mois entre 1970 et 1985.
var QueryResult6 = dc.Students
    .Where(s => s.BirthDate.Year >= 1970 && s.BirthDate.Year <= 1985)
    .GroupBy(s => new { BirthMonth = s.BirthDate.Month })
    .Where(g => g.Count() > 1) // Filtre pour les mois ayant plus d'un étudiant
    .Select(g => new
    {
        BirthMonth = g.Key.BirthMonth,
        AVG_Result = g.Average(s => s.Year_Result)
    });

foreach (var result in QueryResult6)
{
    Console.WriteLine("Mois de naissance : {0}, Résultat moyen : {1}", result.BirthMonth, result.AVG_Result);
}

//Ex 5.5
//Exercice 5.5 
//Donner pour chaque cours le nom du professeur responsable ainsi que la section dont il fait partie.
var QueryResult7 = from c in dc.Courses
                   join p in dc.Professors on c.Professor_ID equals p.Professor_ID
                   select new
                   {
                        Course_name = c.Course_Name,
                        Prof_Name = p.Professor_Name,
                        Section = p.Section_ID,
                   };

foreach(var rep in QueryResult7)
{
    Console.WriteLine("cours: {0}, Prof responsable:{1}, section:{2}", rep.Course_name, rep.Prof_Name, rep.Section);
}

//Ex 5.7
//Donner pour toutes les sections les professeurs qui en sont membres.
var QueryResult8 = from s in dc.Sections
                   join p in dc.Professors on s.Section_ID equals p.Section_ID
                   select new
                   {
                       Section = s.Section_Name,
                       Prof_Name = p.Professor_Name,
                   };



foreach (var sectionGroup in QueryResult8.GroupBy(s => s.Section))
{
    Console.WriteLine("nom cours: {0}", sectionGroup.Key);
    foreach (var rep in sectionGroup)
    {
        Console.WriteLine(rep.Prof_Name);
    }
}







