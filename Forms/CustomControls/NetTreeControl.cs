﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MetroFramework.Controls;
using SatSolver.Objects;
using SatSolver.UserInterface.ApplicationForm;
using SatSolver.Utilities;

namespace SatSolver.UserInterface.CustomControls
{
    public partial class NetTreeControl : MetroUserControl
    {
        public int Id;
        private MainForm _mainForm;
        private ImageList _imageList;
        

        public NetTreeControl()
        {
            InitializeComponent();

            _imageList = new ImageList();
            _imageList.TransparentColor = Color.White;
            _imageList.Images.Add(new Bitmap(Properties.Resources.circuit)); //0
            _imageList.Images.Add(new Bitmap(Properties.Resources.info_16)); //1
            _imageList.Images.Add(new Bitmap(Properties.Resources.wiring)); //2
            _imageList.Images.Add(new Bitmap(Properties.Resources.gate_and)); //3
            _imageList.Images.Add(new Bitmap(Properties.Resources.gate_or)); //4
            _imageList.Images.Add(new Bitmap(Properties.Resources.gate_xor)); //5
            _imageList.Images.Add(new Bitmap(Properties.Resources.gate_inv)); //6
            _imageList.Images.Add(new Bitmap(Properties.Resources.gate_one)); //7
            
            treeView.ImageList = _imageList;
        }



        public void SetParent(MainForm mainForm)
        {
            _mainForm = mainForm;
        }

        public void setId(int id)
        {
            Id = id;
        }

        public void AddCircuit(Circuit circuit)
        {
            Clean();
            treeView.AddCircuit(circuit);
        }

        /// <summary>
        /// Cleans up the control of any loaded file, calculation and setting
        /// </summary>
        public void Clean()
        {
            //Todo cleanup any visuals and logics
            treeView.Nodes.Clear();
            textBoxInfo.Clear();
        }

        private void browseForNetlist_Click(object sender, EventArgs e)
        {
            ShowBrowseNetListFileDialog();            
        }

        public void SetImageList(ImageList treeImageList)
        {
            treeView.ImageList = treeImageList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">
        /// Integer to indicate which TreeNodeControl has called this method, or if it is called by something else,
        /// which TreeNodeControl should be affected. id can only 1 or 2. Because we only have 2 TreeNodeControls.
        /// If you add more TreeNodeControls then change the check at the beggining of the method.
        /// </param>
        public void ShowBrowseNetListFileDialog()
        {
            Circuit circuit = null;

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Open a NetList file for Circuit " + Id;
            ofd.Filter = "NetList Files|*.net";

            //TODO only do this if is DEBUG BUILD!
            ofd.InitialDirectory = System.IO.Path.GetDirectoryName(
            System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                NetListReader reader = new NetListReader(ofd.FileName);
                try
                {
                    circuit = reader.GenerateCircuit();
                }
                catch (InvalidNetListFileException inlfException)
                {
                    MessageBox.Show(inlfException.ToString(), "Invalid NetList File", MessageBoxButtons.OK,
                        MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Critical Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }

            if (circuit != null)
            {
                AddCircuit(circuit);
            }
        }

        private void expandNodes_Click(object sender, EventArgs e)
        {
            treeView.ExpandAll();
        }

        private void collapseNodes_Click(object sender, EventArgs e)
        {
            treeView.CollapseAll();
        }
    }
}