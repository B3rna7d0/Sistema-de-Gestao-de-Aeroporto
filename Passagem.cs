using System;

public class Passagem
{
    // Atributos
    public int Id { get; set; } // Identificador único da passagem
    public Voo Voo { get; set; } // Associação ao voo reservado
    public string Passageiro { get; set; } // Nome do passageiro
    public bool CheckInRealizado { get; private set; } // Indica se o check-in foi realizado

    // Construtor
    public Passagem(int id, Voo voo, string passageiro)
    {
        Id = id;
        Voo = voo;
        Passageiro = passageiro;
        CheckInRealizado = false; // Inicialmente, check-in não realizado
    }

    // Método para realizar o check-in
    public bool FazerCheckIn()
    {
        if (!CheckInRealizado)
        {
            CheckInRealizado = true;
            Console.WriteLine($"Check-in realizado com sucesso para o passageiro: {Passageiro}");
            return true;
        }
        else
        {
            Console.WriteLine($"Check-in já foi realizado para o passageiro: {Passageiro}");
            return false;
        }
    }

    // Método para exibir informações da passagem
    public void MostrarInformacoes()
    {
        Console.WriteLine($"Passagem ID: {Id}");
        Console.WriteLine($"Passageiro: {Passageiro}");
        Console.WriteLine($"Voo: {Voo.Id} - {Voo.Origem} -> {Voo.Destino}");
        Console.WriteLine($"Horário: {Voo.Horario}");
        Console.WriteLine($"Check-in realizado: {(CheckInRealizado ? "Sim" : "Não")}");
        Console.WriteLine("--------------------------------------------");
    }
}
