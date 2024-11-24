using System;

public class Voo
{
    // Atributos
    public int Id { get; set; } // Identificador único do voo
    public string Origem { get; set; } // Local de partida
    public string Destino { get; set; } // Local de chegada
    public DateTime Horario { get; set; } // Horário do voo
    public int CapacidadeBagagem { get; set; } // Capacidade total de bagagem (em quilos)
    public int BagagemAtual { get; set; } // Peso total de bagagem já registrado
    public int PassagensDisponiveis { get; set; } // Número de passagens disponíveis

    // Construtor
    public Voo(int id, string origem, string destino, DateTime horario, int capacidadeBagagem, int passagensDisponiveis)
    {
        Id = id;
        Origem = origem;
        Destino = destino;
        Horario = horario;
        CapacidadeBagagem = capacidadeBagagem;
        BagagemAtual = 0; // Inicialmente, sem bagagem registrada
        PassagensDisponiveis = passagensDisponiveis;
    }

    // Método para verificar se há espaço para bagagem adicional
    public bool VerificarBagagemDisponivel(int peso)
    {
        return BagagemAtual + peso <= CapacidadeBagagem;
    }

    // Método para registrar bagagem
    public bool RegistrarBagagem(int peso)
    {
        if (VerificarBagagemDisponivel(peso))
        {
            BagagemAtual += peso;
            return true;
        }
        return false;
    }

    // Método para reservar uma passagem
    public bool ReservarPassagem()
    {
        if (PassagensDisponiveis > 0)
        {
            PassagensDisponiveis--;
            return true;
        }
        return false;
    }

    // Método para exibir informações do voo
    public void MostrarInformacoes()
    {
        Console.WriteLine($"Voo ID: {Id}");
        Console.WriteLine($"Origem: {Origem}");
        Console.WriteLine($"Destino: {Destino}");
        Console.WriteLine($"Horário: {Horario}");
        Console.WriteLine($"Passagens Disponíveis: {PassagensDisponiveis}");
        Console.WriteLine($"Capacidade de Bagagem: {BagagemAtual}/{CapacidadeBagagem}");
        Console.WriteLine("--------------------------------------------");
    }
}
