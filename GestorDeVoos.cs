using System;
using System.Collections.Generic;
using System.IO;

public class GestorDeVoos
{
    // Listas para armazenar os dados
    public List<Voo> VoosDisponiveis { get; private set; }
    public List<Passagem> PassagensReservadas { get; private set; }

    // Caminhos para os arquivos
    private string caminhoVoos = "database/voos.txt";
    private string caminhoPassagens = "database/passagens.txt";

    // Construtor
    public GestorDeVoos()
    {
        VoosDisponiveis = new List<Voo>();
        PassagensReservadas = new List<Passagem>();
        CarregarDados(); // Carrega os dados ao inicializar
    }

    // Método para carregar dados dos arquivos
    public void CarregarDados()
    {
        // Carregar voos
        if (File.Exists(caminhoVoos))
        {
            var linhas = File.ReadAllLines(caminhoVoos);
            foreach (var linha in linhas)
            {
                var partes = linha.Split(';');
                if (partes.Length == 6)
                {
                    int id = int.Parse(partes[0]);
                    string origem = partes[1];
                    string destino = partes[2];
                    DateTime horario = DateTime.Parse(partes[3]);
                    int capacidadeBagagem = int.Parse(partes[4]);
                    int passagensDisponiveis = int.Parse(partes[5]);
                    VoosDisponiveis.Add(new Voo(id, origem, destino, horario, capacidadeBagagem, passagensDisponiveis));
                }
            }
        }

        // Carregar passagens
        if (File.Exists(caminhoPassagens))
        {
            var linhas = File.ReadAllLines(caminhoPassagens);
            foreach (var linha in linhas)
            {
                var partes = linha.Split(';');
                if (partes.Length == 3)
                {
                    int idPassagem = int.Parse(partes[0]);
                    int idVoo = int.Parse(partes[1]);
                    string passageiro = partes[2];
                    var voo = BuscarVooPorId(idVoo);
                    if (voo != null)
                    {
                        PassagensReservadas.Add(new Passagem(idPassagem, voo, passageiro));
                    }
                }
            }
        }
    }

    // Método para salvar os dados nos arquivos
    public void SalvarDados()
    {
        // Salvar voos
        var linhasVoos = new List<string>();
        foreach (var voo in VoosDisponiveis)
        {
            linhasVoos.Add($"{voo.Id};{voo.Origem};{voo.Destino};{voo.Horario};{voo.CapacidadeBagagem};{voo.PassagensDisponiveis}");
        }
        File.WriteAllLines(caminhoVoos, linhasVoos);

        // Salvar passagens
        var linhasPassagens = new List<string>();
        foreach (var passagem in PassagensReservadas)
        {
            linhasPassagens.Add($"{passagem.Id};{passagem.Voo.Id};{passagem.Passageiro}");
        }
        File.WriteAllLines(caminhoPassagens, linhasPassagens);
    }

    // Método para buscar um voo pelo ID
    public Voo BuscarVooPorId(int id)
    {
        return VoosDisponiveis.Find(v => v.Id == id);
    }

    // Método para mostrar voos disponíveis
    public void MostrarVoosDisponiveis()
    {
        Console.WriteLine("Voos disponíveis:");
        foreach (var voo in VoosDisponiveis)
        {
            voo.MostrarInformacoes();
        }
    }

    // Método para comprar uma passagem
    public Passagem ComprarPassagem(int idVoo, string nomePassageiro)
    {
        var voo = BuscarVooPorId(idVoo);
        if (voo != null && voo.ReservarPassagem())
        {
            int idPassagem = PassagensReservadas.Count + 1;
            var novaPassagem = new Passagem(idPassagem, voo, nomePassageiro);
            PassagensReservadas.Add(novaPassagem);
            Console.WriteLine("Passagem comprada com sucesso!");
            SalvarDados();
            return novaPassagem;
        }
        else
        {
            Console.WriteLine("Não foi possível comprar a passagem. Voo inexistente ou sem vagas.");
            return null;
        }
    }
}
