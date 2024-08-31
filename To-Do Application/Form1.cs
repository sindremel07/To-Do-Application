using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace To_Do_Application
{
    public partial class ToDoApp : Form
    {
        public ToDoApp()
        {
            InitializeComponent();
        }

        DataTable todoList = new DataTable();
        bool isEditing = false;

        private void ToDoApp_Load(object sender, EventArgs e)
        {
            // Create columns
            todoList.Columns.Add("Title");
            todoList.Columns.Add("Description");

            // Point our datagridview to our datasource
            toDoListView.DataSource = todoList;
        }

        private void newButton_Click(object sender, EventArgs e)
        {
            titleTextBox.Text = "";
            descriptionTextBox.Text = "";
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            isEditing = true;
            // Fill text fields with data from table
            titleTextBox.Text = todoList.Rows[toDoListView.CurrentCell.RowIndex].ItemArray[0].ToString();
            descriptionTextBox.Text = todoList.Rows[toDoListView.CurrentCell.RowIndex].ItemArray[1].ToString();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                todoList.Rows[toDoListView.CurrentCell.RowIndex].Delete();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (isEditing)
            {
                if (titleTextBox.Text == "" | descriptionTextBox.Text == "")
                {
                    const string message = "Are you sure that you would like to delete this row?";
                    const string caption = "Delete row";
                    var result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.No) 
                    { 
                        return; 
                    }
                    else
                    {
                        todoList.Rows[toDoListView.CurrentCell.RowIndex].Delete();
                    }
                }
                else
                {
                    todoList.Rows[toDoListView.CurrentCell.RowIndex]["Title"] = titleTextBox.Text;
                    todoList.Rows[toDoListView.CurrentCell.RowIndex]["Description"] = descriptionTextBox.Text;
                }
            }
            else
            {
                if (titleTextBox.Text == "" | descriptionTextBox.Text == "")
                {
                    MessageBox.Show("All fields required!", "Error", MessageBoxButtons.OK);
                }
                else
                {
                    todoList.Rows.Add(titleTextBox.Text, descriptionTextBox.Text);
                }
            }
            // Clear fields
            titleTextBox.Text = "";
            descriptionTextBox.Text = "";
            isEditing = false;
        }
    }
}
