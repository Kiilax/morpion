using System;

class Morpion
{
    // Tableau statique représentant la grille du morpion, initialisé avec des espaces vides
    private static readonly string[] grille = { " ", " ", " ", " ", " ", " ", " ", " ", " " };
    // Variable statique pour stocker le gagnant
    private static string winner = "Personne n'a gagné !";

    // Point d'entrée du programme
    public static void Main()
    {
        // Boucle principale pour le jeu, alternant les tours du joueur et de l'IA
        for (int i = 0; i < 5; i++)
        {
            if (!CheckWin()) AskQuestion(); // Tour du joueur
            if (i != 4 && !CheckWin()) RemplirCaseIA(); // Tour de l'IA si ce n'est pas le dernier tour
        }
        CheckWin(); // Vérifie une dernière fois s'il y a un gagnant
        Console.Clear(); // Nettoie la console pour une meilleure lisibilité
        Console.WriteLine(PrintGame()); // Affiche la grille finale
        Console.WriteLine(winner); // Affiche le gagnant
        ResetGame(); // Demande si le joueur veut recommencer
    }

    // Fonction pour demander au joueur de choisir une case
        private static void AskQuestion()
    {
        Console.Clear(); // Nettoie la console
        Console.WriteLine(PrintGame()); // Affiche la grille actuelle
        Console.Write("Choisissez le numéro de la case que vous voulez remplir : ");

        int choice;

        try
        {
            choice = Convert.ToInt32(Console.ReadLine()); // Vérifie si le choix est valide
        }
        catch (Exception)
        {
            Console.Write("Choix invalide, vous devez rentrer un chiffre entre 1 et 9 !\nAppuyez sur Entrée pour réessayer ");
            Console.ReadLine(); // Attend que l'utilisateur appuie sur Entrée
            AskQuestion(); // Relance la fonction
            return; // Sortie pour éviter la récursivité
        }

        if (!RemplirCase(choice))
        {
            Console.WriteLine("Case invalide ! Appuyez sur Entrée pour réessayer.");
            Console.ReadLine(); // Attend que l'utilisateur appuie sur Entrée
            AskQuestion(); // Relance la fonction
        }
    }

    // Fonction pour remplir une case avec "X"
    private static bool RemplirCase(int choice)
    {
        if (choice > 0 && choice < 10) // Vérifie si le choix est dans les limites
        {
            if (grille[choice - 1] == " ") // Vérifie si la case est vide
            {
                grille[choice - 1] = "X"; // Remplit la case avec "X"
                return true; // Retourne vrai si la case a été remplie
            }
        }
        return false; // Retourne faux si la case n'a pas pu être remplie
    }

    // Fonction pour remplir une case avec "O" par l'IA
    private static void RemplirCaseIA()
    {
        Random rd = new Random();
        int choice = rd.Next(9); // Génère un nombre aléatoire entre 0 et 8

        if (grille[choice] == " ")
        {
            grille[choice] = "O"; // Remplit la case avec "O" si elle est vide
        }
        else
        {
            RemplirCaseIA(); // Utilise récursivement jusqu'à trouver une case vide
        }
    }

    // Fonction pour afficher la grille du jeu
    private static string PrintGame()
    {
        string game = " ";

        for (int i = 0; i < grille.Length; i++)
        {
            game += grille[i]; // Ajoute la valeur de la case au string
            if (i == 2 || i == 5)
            {
                // Ajoute les séparateurs pour les lignes et colonnes
                game += "\t" + (i - 1) + " | " + i + " | " + (i + 1) + "\n---+---+---\t--+---+--\n ";
            }
            else if (i != 8)
            {
                game += " | "; // Ajoute le séparateur pour les colonnes
            }
            else if (i == 8)
            {
                game += "\t" + (i - 1) + " | " + i + " | " + (i + 1);
            }
        }
        return game + "\n"; // Retourne la grille formattée
    }

    // Fonction pour vérifier s'il y a un gagnant
    private static bool CheckWin()
    {
        // Vérifie les lignes
        for (int i = 0; i < 9; i += 3)
        {
            if (grille[i] != " " && grille[i] == grille[i + 1] && grille[i + 1] == grille[i + 2])
            {
                return SetWinner(i); // Déclare le gagnant
            }
        }

        // Vérifie les colonnes
        for (int i = 0; i < 3; i++)
        {
            if (grille[i] != " " && grille[i] == grille[i + 3] && grille[i + 3] == grille[i + 6])
            {
                return SetWinner(i); // Déclare le gagnant
            }
        }

        // Vérifie les diagonales
        if (grille[0] != " " && grille[0] == grille[4] && grille[4] == grille[8])
        {
            return SetWinner(0); // Déclare le gagnant
        }

        if (grille[2] != " " && grille[2] == grille[4] && grille[4] == grille[6])
        {
            return SetWinner(2); // Déclare le gagnant
        }

        return false; // Retourne faux s'il n'y a pas de gagnant
    }

    // Fonction pour réinitialiser le jeu
    private static void ResetGame()
    {
        Console.Write("Recommencer ? [0]oui ou [1]non : ");
        int choice;

        try
        {
            choice = Convert.ToInt32(Console.ReadLine()); // Vérifie si le choix est valide
        }
        catch (Exception)
        {
            Console.WriteLine("Choix invalide, vous devez rentrer un chiffre !");
            ResetGame(); // Relance la fonction
            return; // Sortie pour éviter la récursivité
        }

        if (choice == 0)
        {
            // Réinitialise la grille et le gagnant
            for (int i = 0; i < grille.Length; i++)
            {
                grille[i] = " ";
            }
            winner = "Personne n'a gagné !";
            Main(); // Relance le jeu
        }
        else if (choice > 1)
        {
            Console.WriteLine("Choix invalide, vous devez rentrer un chiffre entre 0 et 1 !");
            ResetGame(); // Relance la fonction
        }
    }

    // Fonction pour définir le gagnant
    private static bool SetWinner(int i)
    {
        if (grille[i] == "X") winner = "Tu as gagné !";
        else if (grille[i] == "O") winner = "Tu as perdu !";
        return true; // Retourne vrai si un gagnant a été déclaré
    }
}
