# Groupe-3-
       Résolution par algorithme génétique avec GeneticSharp
       
       
/* 

* Rajae Lakhnati 
* Kaoutar Lehlous 
* Farah Abbes
* soumaya ayatillah 

*/

![image](https://user-images.githubusercontent.com/75318261/113597157-94494a00-963b-11eb-8b14-3494d47d2537.png)


![Uploading image.png…]()


Le monde de l'IA et des algorithmes génétiques d'inspiration biologique repose sur des concepts simplifiés tirés de la biologie et du domaine de la génétique. 


GeneticSharp est une bibliothèque d'algorithmes génétiques C # rapide, extensible, multiplateforme et multithreading qui simplifie le développement d'applications utilisant des algorithmes génétiques (GA). 

Qu'est-ce qu'un algorithme génétique : 

1.Population.                                        Individual []
2.individu (représenté par des gènes)                Int[], char[], My class[]
4.Sélection naturelle                                Float CalculateFitness()
5.la reproduction                                    Individual Crossover()
6.Mutation                                           void Mutate()

Peut être utilisé dans tout type d'applications .NET Core et .NET Framework, comme les jeux ASP .NET MVC, ASP .NET Core, Blazor, Web Forms, UWP, Windows Forms, GTK #, Xamarin et Unity3D.

Notre sujet concerne une résolution par algorithme génétique avec GeneticSharp,Il s’agit d’utiliser une méthode d’exploration stochastique par algorithmes génétiques, comme présenté dans l’article suivant " https://arxiv.org/ftp/arxiv/papers/0805/0805.0697.pdf " en utilisant la librairie GeneticSharp

![image](https://user-images.githubusercontent.com/75318261/113018756-286d6a00-9181-11eb-9773-729ff0a81bae.png)

![image](https://user-images.githubusercontent.com/75318261/113019094-85692000-9181-11eb-89c2-19e15441d600.png)



![image](https://user-images.githubusercontent.com/75318261/113019187-9b76e080-9181-11eb-9f7b-0e5a5282da9c.png)



![image](https://user-images.githubusercontent.com/75318261/113019236-a7fb3900-9181-11eb-8377-83b848332eab.png)


**Résumé de la description de la population**

* croissance de la génération 
* vérifier qui peut mourir
* déterminer qui peut se reproduire
* faire le croisement des gènes et les ajouter à la population
* muter quelques gènes dans la nouvelle population
* calculer l’aptitude de tous les gènes Genomes.Sort();
* tuer tous les gènes au-dessus de la limite de la population
* choisir au hasard les mamans et les pères
* les rendre égaux
* les croiser et les ajouter selon la forme physique
* choisir le meilleur 2 parmi les parents et les enfants
* si Best2 est vrai, ajouter des gènes de fitness
* il suffit d’écrire le top 20
* imprimer toutes les 100 générations

**Voici le résultat obtenu par le programme**

![image](https://user-images.githubusercontent.com/75318261/113020130-96666100-9182-11eb-8585-aeec9bea70a3.png)

![image](https://user-images.githubusercontent.com/75318261/113021264-b34f6400-9183-11eb-9938-c0dc2fba3295.png)



