using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace localizacaoXY
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Adicionando manipuladores de eventos para atualizar a posição do TextBlock quando o texto é alterado
            textBoxX.TextChanged += TextBox_TextChanged;
            textBoxY.TextChanged += TextBox_TextChanged;

            // Adicionando manipulador de evento para o clique do botão "Copiar Coordenadas"
            btnCopiarCoordenadas.Click += BtnCopiarCoordenadas_Click;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(textBoxX.Text, out double x) && double.TryParse(textBoxY.Text, out double y))
            {
                // Limitando o movimento dentro do Border
                double maxX = areaMovimentacao.ActualWidth - movimentacao.ActualWidth;
                double maxY = areaMovimentacao.ActualHeight - movimentacao.ActualHeight;

                // Movendo o TextBlock para as coordenadas X e Y especificadas
                Canvas.SetLeft(movimentacao, Math.Min(Math.Max(0, x), maxX));
                Canvas.SetTop(movimentacao, Math.Min(Math.Max(0, y), maxY));

                // Atualizando o campo de texto com as coordenadas
                textBoxCoordenadas.Text = $"X: {x}, Y: {y}";
            }
        }

        private void TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            // Permitindo apenas entrada numérica
            e.Handled = !IsNumeric(e.Text);
        }

        private bool IsNumeric(string text)
        {
            return double.TryParse(text, out _);
        }

        private void BtnCopiarCoordenadas_Click(object sender, RoutedEventArgs e)
        {
            // Copiando as coordenadas atuais do campo "movimentacao" para a área de transferência
            Clipboard.SetText(textBoxCoordenadas.Text);
        }
    }
}
