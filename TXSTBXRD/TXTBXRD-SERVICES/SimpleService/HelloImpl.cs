using TXSTBXRD_INTERFACES;
using TXSTBXRD_INTERFACES.Models;
using Shed.CoreKit.WebApi;

namespace SimpleService
{
    public class HelloImpl: IHello
    {
        public Hello get() {
            var result = new Hello();
            result.HelloString = "Test MicroService";
            return result;
        }
    }
}