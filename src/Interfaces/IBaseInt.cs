

namespace ms_processoSeletivo.Interfaces
{
    public interface IBaseInt<in T, out A>
    {
        A Adicionar(T obj);
        IEnumerable<A> BuscarTodos();
        A BuscarPorId(int id);
        bool Excluir(int id);
    }
}