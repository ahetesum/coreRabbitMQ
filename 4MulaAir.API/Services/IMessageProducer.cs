using System;
namespace _4MulaAir.API.Services
{
	public interface IMessageProducer
	{
		public void SendingMessage<T>(T message);
	}
}

