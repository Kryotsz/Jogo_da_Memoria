using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatchingGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // chama o método pra assimilar os ícones às suas posições
            AssignIconsToSquare();
        }

        // variável de controle pro primeiro clique
        Label firstClicked = null;

        // variável de controle pro segundo clique
        Label secondClicked = null;

        // seleciona aleatoriamente os ícones da lista
        Random random = new Random();

        // lista de ícones que serão escolhidos pelo Random
        List<string> icons = new List<string>()
        {
            "!","!","N","N",",",",","k","k","l","l","Y","Y","X","X","q","q","W","W",
            "b","b","v","v","w","w","z","z","L","L","j","j","h","h","%","%","@","@"
        };

        // assimila os ícones aos quadrados do jogo
        private void AssignIconsToSquare()
        {
            // seleciona um ícone da lista e adiciona em um lugar aleatório que não possui ícone na label
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconlabel = control as Label;
                if (iconlabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconlabel.Text = icons[randomNumber];
                    // a cor do ícone fica igual ao background, assim, escondendo os ícones
                    iconlabel.ForeColor = iconlabel.BackColor;
                    icons.RemoveAt(randomNumber);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // o timer só ficará ativo depois que dois ícones diferentes forem clicados
            // se o timer estiver ativo, ignore o clique
            if (timer1.Enabled == true)
                return;

            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {
                // se o label clicado é preto, é porque ja foi clicado antes, então ignore o clique
                if (clickedLabel.ForeColor == Color.White)
                    return;

                // se a variável é nula significa que nenhum ícone foi clicado anteriormente
                if (firstClicked == null)
                {
                    // portanto esse será o primeiro ícone
                    firstClicked = clickedLabel;
                    // muda a cor pra branco pra saber que esse ícone ja foi clicado
                    firstClicked.ForeColor = Color.White;

                    return;
                }

                // esse será o segundo ícone clicado
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.White;

                // chamará o método para verificar se o usuário acertou
                CheckForWinner();

                // se o usuário clicou em dois ícones iguais
                if (firstClicked.Text == secondClicked.Text)
                {
                    // resetar as variáveis pro usuário continuar o jogo
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }

                // iniciar timer quando o usuário clicar em dois ícones não correspondentes
                timer1.Start();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // o timer para
            timer1.Stop();

            // esconde ambos os ícones clicados
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            // reseta as variáveis para o usuário poder clicar em novos ícones
            firstClicked = null;
            secondClicked = null;
        }

        private void CheckForWinner()
        {
            // o loop passará por cada icone, verificando se o usuário acertou ou não
            foreach(Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    // verifica se o ícone está visível ou não
                    // se não estiver visível, é porque o usuário errou, então o return é acionado
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }

            // se passou pelo loop sem dar return, significa que todos os ícones estão revelados
            // apresentar a mensagem de parabéns e fechar o jogo
            MessageBox.Show("Você adivinhou todos os ícones!", "Parabéns");
            Close();
        }
    }
}
