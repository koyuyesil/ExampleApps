Public Class Form1
    Dim sayac As Integer



    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.BackColor = System.Drawing.Color.DarkRed
    End Sub

    Private Sub Form1_MouseUp(sender As Object, e As MouseEventArgs) Handles MyBase.MouseUp
        Me.BackColor = System.Drawing.Color.DarkViolet
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        sayac = sayac + 1
        Me.Label1.Text = "Sayac degeri;" & sayac
        If sayac >= 100 Then
            kendiislevimiz()
        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Timer1.Start()
        Me.Label2.Text = "Sayac Basladi"
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Timer1.Stop()
        Me.Label2.Text = "Sayac Duraklatildi"
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        sayac = 0
        Me.Label1.Text = "Sayac degeri; 0"
        Me.Label2.Text = "Sayac Sifirlandi"
    End Sub

    Private Sub kendiislevimiz()
        Me.Label3.Text = "Sayac 100 e ulasti"
        Me.BackColor = System.Drawing.Color.DarkViolet
    End Sub


End Class
