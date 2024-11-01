namespace salaoDaLora.Models
{
    public class Agendamento
    {
        public int AgendamentoId { get; set; }
        public DateTime DataAgendamento {  get; set; }

        public int ClienteId {  get; set; } //chave esrangeira cliente
        public Cliente Cliente { get; set; }

        public int ProfissionalId { get; set; } //chave estrangeira profissional
        public Profissional Profissional { get; set; }

        public int ServicoId { get; set; } //chave estrangeira serviço
        public Servico Servico { get; set; }
       

    }
}

