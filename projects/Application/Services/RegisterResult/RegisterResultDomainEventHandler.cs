using System.Threading;
using System.Threading.Tasks;
using Keeper.Application.Contract;
using Keeper.Domain.Events;
using MediatR;

namespace Keeper.Application.Services.RegisterResult
{
	public class RegisterResultDomainEventHandler : INotificationHandler<RegisterResultEvent>
	{
		private readonly IUnitOfWork _uow;

		public RegisterResultDomainEventHandler(IUnitOfWork uow)
		{
			_uow = uow;
		}
		public async Task Handle(RegisterResultEvent notification, CancellationToken cancellationToken)
		{
			var matchAnalyse = notification.Match;
			//TODO Atualizar Classificação
			//TODO Quando tiver acabado o grupo
			//TODO Atualiza status dos times envolvidos
			//TODO Se a Próxima Fase Tiver regulamento Pré definido já inscreve os times nos lugares
			//TODO Se A Fase tiver Terminado e a próxima tiver regulamento melhores vs piores faz o
			//TODO ranking e inscreve os times nos devidos lugares
			//TODO libera operação de Realizar Sorteio
			//TODO Se fase terminou Caso seja ultima partida do campeonato atualiza o STATUS
		}
	}
}
