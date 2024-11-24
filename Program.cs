using System;

class Program
{
    static void Main(string[] args)
    {
        GestorDeVoos gestor = new GestorDeVoos();

        while (true)
        {
            Console.WriteLine("=== Sistema de Gestão de Voos ===");
            Console.WriteLine("1. Mostrar voos disponíveis");
            Console.WriteLine("2. Comprar passagem");
            Console.WriteLine("3. Fazer check-in");
            Console.WriteLine("4. Despachar bagagem");
            Console.WriteLine("5. Sair");
            Console.Write("Escolha uma opção: ");

            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    gestor.MostrarVoosDisponiveis();
                    break;

                case "2":
                    ComprarPassagem(gestor);
                    break;

                case "3":
                    FazerCheckIn(gestor);
                    break;

                case "4":
                    DespacharBagagem(gestor);
                    break;

                case "5":
                    Console.WriteLine("Saindo do sistema...");
                    gestor.SalvarDados();
                    return;

                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }
    }

    static void ComprarPassagem(GestorDeVoos gestor)
    {
        Console.WriteLine("=== Comprar Passagem ===");
        gestor.MostrarVoosDisponiveis();

        Console.Write("Digite o ID do voo desejado: ");
        if (int.TryParse(Console.ReadLine(), out int idVoo))
        {
            Console.Write("Digite o nome do passageiro: ");
            string nomePassageiro = Console.ReadLine();

            var passagem = gestor.ComprarPassagem(idVoo, nomePassageiro);
            if (passagem != null)
            {
                Console.WriteLine("Passagem comprada com sucesso!");
                passagem.MostrarInformacoes();
            }
        }
        else
        {
            Console.WriteLine("ID inválido. Operação cancelada.");
        }
    }

    static void FazerCheckIn(GestorDeVoos gestor)
    {
        Console.WriteLine("=== Fazer Check-In ===");
        Console.Write("Digite o ID da sua passagem: ");
        if (int.TryParse(Console.ReadLine(), out int idPassagem))
        {
            var passagem = gestor.PassagensReservadas.Find(p => p.Id == idPassagem);
            if (passagem != null)
            {
                passagem.FazerCheckIn();
            }
            else
            {
                Console.WriteLine("Passagem não encontrada.");
            }
        }
        else
        {
            Console.WriteLine("ID inválido. Operação cancelada.");
        }
    }

    static void DespacharBagagem(GestorDeVoos gestor)
    {
        Console.WriteLine("=== Despachar Bagagem ===");
        Console.Write("Digite o ID da sua passagem: ");
        if (int.TryParse(Console.ReadLine(), out int idPassagem))
        {
            var passagem = gestor.PassagensReservadas.Find(p => p.Id == idPassagem);
            if (passagem != null && passagem.CheckInRealizado)
            {
                Console.Write("Digite o peso da bagagem (em kg): ");
                if (int.TryParse(Console.ReadLine(), out int peso))
                {
                    if (passagem.Voo.RegistrarBagagem(peso))
                    {
                        Console.WriteLine("Bagagem despachada com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("Capacidade de bagagem do voo excedida. Não foi possível despachar.");
                    }
                }
                else
                {
                    Console.WriteLine("Peso inválido. Operação cancelada.");
                }
            }
            else
            {
                Console.WriteLine("Passagem não encontrada ou check-in ainda não realizado.");
            }
        }
        else
        {
            Console.WriteLine("ID inválido. Operação cancelada.");
        }
    }
}
