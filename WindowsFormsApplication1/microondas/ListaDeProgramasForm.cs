﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication1.Microondas;
using WindowsFormsApplication1.Service;


namespace WindowsFormsApplication1.Programas
{
    public partial class ListaDeProgramasForm : Form
    {
        ProgramasCadastrados programas;
        ProgramaService programaService;
        public ListaDeProgramasForm(ProgramasCadastrados programas)
        {
            InitializeComponent();
            this.programaService = new ProgramaService(programas);
            this.programas = programaService.buscarProgramas();
            foreach(Programa programa in this.programas.buscarTodos()){
                programasListView.Items.Add(programa.getNome());
            }
        }

        private void CadastroDeProgramasForm_Load(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cadastrarButton_Click(object sender, EventArgs e)
        {
            this.Close();
            new ProgramaForm(programaService).Show();
        }

        private void programasListView_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                Programa programa = programaService.buscarPorNome(programasListView.SelectedItems[0].Text.ToUpper());
                this.Close();
                new ProgramaForm(programaService,programa,true).Show();            
            }catch(Exception buscarException){
                MessageBox.Show(
                    buscarException.Message
                    );
            }
        }

        private void deletarButton_Click(object sender, EventArgs e)
        {
            try
            {
                Programa programa = programaService.buscarPorNome(programasListView.SelectedItems[0].Text.ToUpper());
                programaService.remover(programa);
                atualizaListView();
            }catch(Exception removerException){
                MessageBox.Show(
                    removerException.Message
                    );
            }
        }
        private void atualizaListView()
        {
            this.programasListView.Items.Clear();
            foreach (Programa programa in this.programas.buscarTodos())
            {
                programasListView.Items.Add(programa.getNome());
            }
        }
    }
}
