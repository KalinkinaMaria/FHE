﻿#pragma checksum "..\..\..\Controls\AbstractHierarchyNode.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "0842C68EFF7DB00B32CA75F1C3D6ED40"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace FHE.Controls {
    
    
    /// <summary>
    /// AbstractHierarchyNode
    /// </summary>
    public partial class AbstractHierarchyNode : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 8 "..\..\..\Controls\AbstractHierarchyNode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid parent;
        
        #line default
        #line hidden
        
        
        #line 9 "..\..\..\Controls\AbstractHierarchyNode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Ellipse formNode;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\..\Controls\AbstractHierarchyNode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock textNode;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\Controls\AbstractHierarchyNode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button deleteNode;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\Controls\AbstractHierarchyNode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button addFuncLink;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\Controls\AbstractHierarchyNode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button addMembershipFunc;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\Controls\AbstractHierarchyNode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button renameNode;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\Controls\AbstractHierarchyNode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button addEdge;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/FHE;component/controls/abstracthierarchynode.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Controls\AbstractHierarchyNode.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.parent = ((System.Windows.Controls.Grid)(target));
            
            #line 8 "..\..\..\Controls\AbstractHierarchyNode.xaml"
            this.parent.MouseEnter += new System.Windows.Input.MouseEventHandler(this.Grid_MouseEnter);
            
            #line default
            #line hidden
            
            #line 8 "..\..\..\Controls\AbstractHierarchyNode.xaml"
            this.parent.MouseLeave += new System.Windows.Input.MouseEventHandler(this.Grid_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 2:
            this.formNode = ((System.Windows.Shapes.Ellipse)(target));
            return;
            case 3:
            this.textNode = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.deleteNode = ((System.Windows.Controls.Button)(target));
            
            #line 11 "..\..\..\Controls\AbstractHierarchyNode.xaml"
            this.deleteNode.Click += new System.Windows.RoutedEventHandler(this.deleteNode_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.addFuncLink = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\..\Controls\AbstractHierarchyNode.xaml"
            this.addFuncLink.Click += new System.Windows.RoutedEventHandler(this.addFuncLink_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.addMembershipFunc = ((System.Windows.Controls.Button)(target));
            
            #line 29 "..\..\..\Controls\AbstractHierarchyNode.xaml"
            this.addMembershipFunc.Click += new System.Windows.RoutedEventHandler(this.addMembershipFunc_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.renameNode = ((System.Windows.Controls.Button)(target));
            
            #line 38 "..\..\..\Controls\AbstractHierarchyNode.xaml"
            this.renameNode.Click += new System.Windows.RoutedEventHandler(this.renameNode_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.addEdge = ((System.Windows.Controls.Button)(target));
            
            #line 47 "..\..\..\Controls\AbstractHierarchyNode.xaml"
            this.addEdge.Click += new System.Windows.RoutedEventHandler(this.addEdge_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
