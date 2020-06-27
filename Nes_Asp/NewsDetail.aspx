<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsDetail.aspx.cs" Inherits="Nes_Asp.NewsDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link
        rel="stylesheet"
        href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.12.0-2/css/all.min.css" />

    <link rel="stylesheet" href="./style.css" />
    <title>Haber Detay | DHA News</title>
</head>
<body>
    <form id="form1" runat="server">

        <div class="main-container news-detail-container">
            <section class="sidebar">
                <div class="logo-container">
                    <h1 class="logo"><a href="./Home.aspx">NEWS</a></h1>
                </div>
                <nav class="nav-container">
                    <label ID="time" runat="server" class="time">15 Haziran Pazartesi</label>
                    <ul>
                        <li><i class="fas fa-bullhorn"></i><a runat="server" ID="Dishaberler" onclick="getDisHaber">Dış Haberler</a></li>
                        <li><i class="fas fa-university"></i><a runat="server" ID="Egitim" onclick="getEgitim">Eğitim</a></li>
                        <li><i class="fas fa-lira-sign"></i><a runat="server" ID="Ekonomi" onclick="getEkonomi">Ekonomi</a></li>
                        <li><i class="fas fa-globe-europe"></i><a runat="server" ID="Gundem" onclick="getGundem">Gündem</a></li>
                        <li><i class="fas fa-fire"></i><a runat="server" ID="Magazin" onclick="getMagazin">Magazin</a></li>
                        <li><i class="fas fa-globe"></i><a runat="server" ID="Siyaset" onclick="getSiyaset">Siyaset</a></li>
                        <li class="nav-item"><i class="fas fa-futbol"></i><a runat="server" ID="Spor" onclick="getSpor">Spor</a></li>
                    </ul>
                </nav>
            </section>
            
            <asp:Panel ID="newsDetailArea" runat="server"></asp:Panel>
            

        </div>
    </form>
</body>
</html>
