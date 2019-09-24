<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DSAWebseiteTest._Default" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .vertical-center {
            vertical-align: middle;
            TEXT-ALIGN: center;
        }

        .attribut-div {
            float: left;
            padding-right: 3em;
        }
        
        .description-value{
            width:125px;
            display:inline-block;
            vertical-align: middle;
            TEXT-ALIGN: center;
        }
        
    </style>

    <div>
        <div style="float: right">
            <div>
                <div class="attribut-div">
                    <asp:Label ID="LabelMut" Text="Mut" runat="server" Font-Size="Medium" BorderWidth="1" BorderColor="Black" CssClass="description-value"></asp:Label>
                    <asp:Label ID="LabelMutValue" Text="0" runat="server" Width="45" Font-Size="Medium" BorderWidth="1" BorderColor="Black" CssClass="vertical-center"></asp:Label>

                    <asp:Button ID="ButtonMutPlus" Text="+" runat="server" Width="32px" Height="32px" BorderColor="Green" CssClass="vertical-center" OnClick="ButtonMutPlus_Click" />
                    <asp:Button ID="ButtonMutMinus" Text="-" runat="server" Width="32px" Height="32px" BorderColor="Red" CssClass="vertical-center" OnClick="ButtonMutMinus_Click" />
                </div>
                <div class="attribut-div">
                    <asp:Label ID="LabelKlugheit" Text="Klugheit" runat="server" Font-Size="Medium" BorderWidth="1" BorderColor="Black" CssClass="description-value"></asp:Label>
                    <asp:Label ID="LabelKlugheitValue" Text="0" runat="server" Width="45" Font-Size="Medium" BorderWidth="1" BorderColor="Black" CssClass="vertical-center"></asp:Label>

                    <asp:Button ID="ButtonKlugheitPlus" Text="+" runat="server" Width="32px" Height="32px" BorderColor="Green" CssClass="vertical-center" OnClick="ButtonKlugheitPlus_Click" />
                    <asp:Button ID="ButtonKlugheitMinus" Text="-" runat="server" Width="32px" Height="32px" BorderColor="Red" CssClass="vertical-center" OnClick="ButtonKlugheitMinus_Click"/>
                </div>
                <div class="attribut-div">
                    <asp:Label ID="LabelIntuition" Text="Intuition" runat="server" Font-Size="Medium" BorderWidth="1" BorderColor="Black" CssClass="description-value"></asp:Label>
                    <asp:Label ID="LabelIntuitionValue" Text="0" runat="server" Width="45" Font-Size="Medium" BorderWidth="1" BorderColor="Black" CssClass="vertical-center"></asp:Label>

                    <asp:Button ID="ButonIntuitionPlus" Text="+" runat="server" Width="32px" Height="32px" BorderColor="Green" CssClass="vertical-center" OnClick="ButonIntuitionPlus_Click" />
                    <asp:Button ID="ButonIntuitionMinus" Text="-" runat="server" Width="32px" Height="32px" BorderColor="Red" CssClass="vertical-center" OnClick="ButonIntuitionMinus_Click" />
                </div>
            </div>
            <div>
                <div class="attribut-div">
                    <asp:Label ID="LabelCharisma" Text="Charisma" runat="server" Font-Size="Medium" BorderWidth="1" BorderColor="Black" CssClass="description-value"></asp:Label>
                    <asp:Label ID="LabelValueCharisma" Text="0" runat="server" Width="45" Font-Size="Medium" BorderWidth="1" BorderColor="Black" CssClass="vertical-center"></asp:Label>

                    <asp:Button ID="ButtonCharismaPlus" Text="+" runat="server" Width="32px" Height="32px" BorderColor="Green" CssClass="vertical-center" OnClick="ButtonCharismaPlus_Click" />
                    <asp:Button ID="ButtonCharismaMinus" Text="-" runat="server" Width="32px" Height="32px" BorderColor="Red" CssClass="vertical-center" OnClick="ButtonCharismaMinus_Click" />
                </div>
                <div class="attribut-div">
                    <asp:Label ID="LabelFingerfertigkeit" Text="Fingerfertigkeit" runat="server" Font-Size="Medium" BorderWidth="1" BorderColor="Black" CssClass="description-value"></asp:Label>
                    <asp:Label ID="LabelValueFingerfertigkeit" Text="0" runat="server" Width="45" Font-Size="Medium" BorderWidth="1" BorderColor="Black" CssClass="vertical-center"></asp:Label>

                    <asp:Button ID="ButtonFingerfertigkeitPlus" Text="+" runat="server" Width="32px" Height="32px" BorderColor="Green" CssClass="vertical-center" OnClick="ButtonFingerfertigkeitPlus_Click" />
                    <asp:Button ID="ButtonFingerfertigkeitMinus" Text="-" runat="server" Width="32px" Height="32px" BorderColor="Red" CssClass="vertical-center" OnClick="ButtonFingerfertigkeitMinus_Click"/>
                </div>
                <div class="attribut-div">
                    <asp:Label ID="LabelGewandheit" Text="Gewandheit" runat="server" Font-Size="Medium" BorderWidth="1" BorderColor="Black" CssClass="description-value"></asp:Label>
                    <asp:Label ID="LabelValueGewandheit" Text="0" runat="server" Width="45" Font-Size="Medium" BorderWidth="1" BorderColor="Black" CssClass="vertical-center"></asp:Label>

                    <asp:Button ID="ButtonGewandheitPlus" Text="+" runat="server" Width="32px" Height="32px" BorderColor="Green" CssClass="vertical-center" OnClick="ButtonGewandheitPlus_Click" />
                    <asp:Button ID="ButtonGewandheitMinus" Text="-" runat="server" Width="32px" Height="32px" BorderColor="Red" CssClass="vertical-center" OnClick="ButtonGewandheitMinus_Click" />
                </div>
            </div>
            <div>
                <div class="attribut-div">
                    <asp:Label ID="LabelKonstitution" Text="Konstitution" runat="server" Font-Size="Medium" BorderWidth="1" BorderColor="Black" CssClass="description-value"></asp:Label>
                    <asp:Label ID="Label1KonstitutionValue" Text="0" runat="server" Width="45" Font-Size="Medium" BorderWidth="1" BorderColor="Black" CssClass="vertical-center"></asp:Label>

                    <asp:Button ID="ButtonKonstitutionPlus" Text="+" runat="server" Width="32px" Height="32px" BorderColor="Green" CssClass="vertical-center" OnClick="ButtonKonstitutionPlus_Click" />
                    <asp:Button ID="ButtonKonstitutionMinus" Text="-" runat="server" Width="32px" Height="32px" BorderColor="Red" CssClass="vertical-center" OnClick="ButtonKonstitutionMinus_Click" />
                </div>
                <div class="attribut-div">
                    <asp:Label ID="LabelKörperkraft" Text="Körperkraft" runat="server" Font-Size="Medium" BorderWidth="1" BorderColor="Black" CssClass="description-value"></asp:Label>
                    <asp:Label ID="LabelKörperkraftValue" Text="0" runat="server" Width="45" Font-Size="Medium" BorderWidth="1" BorderColor="Black" CssClass="vertical-center"></asp:Label>

                    <asp:Button ID="ButtonKörperkraftPlus" Text="+" runat="server" Width="32px" Height="32px" BorderColor="Green" CssClass="vertical-center" OnClick="ButtonKörperkraftPlus_Click" />
                    <asp:Button ID="ButtonKörperkraftMinus" Text="-" runat="server" Width="32px" Height="32px" BorderColor="Red" CssClass="vertical-center" OnClick="ButtonKörperkraftMinus_Click"/>
                </div>
                <div class="attribut-div">
                    <asp:Label ID="LabelSozialstatus" Text="Sozialstatus" runat="server" Font-Size="Medium" BorderWidth="1" BorderColor="Black" CssClass="description-value"></asp:Label>
                    <asp:Label ID="LabelSozialstatusValue" Text="0" runat="server" Width="45" Font-Size="Medium" BorderWidth="1" BorderColor="Black" CssClass="vertical-center"></asp:Label>

                    <asp:Button ID="ButtonSozialstatusPlus" Text="+" runat="server" Width="32px" Height="32px" BorderColor="Green" CssClass="vertical-center" OnClick="ButtonSozialstatusPlus_Click" />
                    <asp:Button ID="ButtonSozialstatusMinus" Text="-" runat="server" Width="32px" Height="32px" BorderColor="Red" CssClass="vertical-center" OnClick="ButtonSozialstatusMinus_Click" />
                </div>
            </div>
        </div>
        <asp:GridView runat="server" ID="DSATalente"
            ItemType="DSAWebseiteTest.InnerAbstractTalentGeneral"
            SelectMethod="DSATalente_GetData"
            AutoGenerateColumns="false">
            <Columns>
                <asp:DynamicField DataField="Name" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%#  Bind("ProbeString")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ProbeValueString" />

            </Columns>

            <%--</Columns>--%>
        </asp:GridView>
    </div>
</asp:Content>
