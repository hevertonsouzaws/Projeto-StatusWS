namespace PrimeiroPrograma
{
    internal class Produto
    {
        private string _descricao;
        public double Preco { get; private set; }
        public int Quantidade { get; private set; }
   
        
        public Produto(string descricao, double preco, int quantidade)
        {
            _descricao = descricao;
            Preco = preco;
            Quantidade = quantidade;
        }


        // Sem auto implementar
       
        public double ValorTotalEmEstoque()
        {
            return Preco * Quantidade;
        }

        public void AdicionarProduto(int quantidade)
        {
            Quantidade += quantidade;
        }

        public void RemoverProduto(int quantidade)
        {
            Quantidade -= quantidade;
        }

        // Get e set sem propiedades

        public string GetDescricao()
        {
            return _descricao;
        }

        public void SetDescricao(string descricao)
        {
            if (descricao != null && descricao.Length > 1 )
            {
                _descricao = descricao;
            } 
        }

        //Get e Set com propiedade

        public string Descricao 
        {
            get { return _descricao; }
            set
            {
                if (value != null && value.Length > 1)
                {
                    _descricao = value;
                }
            }
        }

        
       
        // Método para exibir as informações do produto 
        public override string ToString()
        {
            return _descricao 
                + "\n | preço: R$ " 
                + Preco.ToString("F2")
                + "\n | estoque: "
                + Quantidade
                + "\n | valor total: R$ "
                + ValorTotalEmEstoque();
        }
      
    }
}
