Option Explicit On
Option Infer Off
Option Strict On

Public Class Form1
    Structure gameCard
        Public button As Button
        Public value As Integer
    End Structure

    Dim card1 As gameCard
    Dim card2 As gameCard
    Dim card3 As gameCard
    Dim card4 As gameCard
    Dim card5 As gameCard
    Dim card6 As gameCard
    Dim card7 As gameCard
    Dim card8 As gameCard
    Dim card9 As gameCard
    Dim card10 As gameCard
    Dim card11 As gameCard
    Dim card12 As gameCard
    Dim card13 As gameCard
    Dim card14 As gameCard
    Dim card15 As gameCard
    Dim card16 As gameCard
    Dim cardArray(15) As gameCard
    Dim randomArray(1, 7) As Integer
    Dim hScores(9) As String

    Dim selectionCount As Integer
    Dim activeCardOne As gameCard
    Dim activeCardTwo As gameCard
    Dim score As Integer
    Dim numMatches As Integer

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        initializeGame()
    End Sub

    Sub initializeGame()
        card1.button = Button1
        cardArray(0) = card1
        card2.button = Button2
        cardArray(1) = card2
        card3.button = Button3
        cardArray(2) = card3
        card4.button = Button4
        cardArray(3) = card4
        card5.button = Button5
        cardArray(4) = card5
        card6.button = Button6
        cardArray(5) = card6
        card7.button = Button7
        cardArray(6) = card7
        card8.button = Button8
        cardArray(7) = card8
        card9.button = Button9
        cardArray(8) = card9
        card10.button = Button10
        cardArray(9) = card10
        card11.button = Button11
        cardArray(10) = card11
        card12.button = Button12
        cardArray(11) = card12
        card13.button = Button13
        cardArray(12) = card13
        card14.button = Button14
        cardArray(13) = card14
        card15.button = Button15
        cardArray(14) = card15
        card16.button = Button16
        cardArray(15) = card16

        Dim rand As New Random
        selectionCount = 0
        score = 0
        lblScore.Text = "Score: "
        numMatches = 0

        For i As Integer = 0 To 7
            Dim reset As Boolean = False
            Dim num As Integer = rand.Next(1, 101)

            For j As Integer = 0 To 7
                If randomArray(0, j) = num Then
                    i -= 1
                    reset = True
                    Exit For
                End If
            Next
            If reset <> True Then
                randomArray(0, i) = num
                randomArray(1, i) = num
            Else
                System.Diagnostics.Debug.WriteLine("duplicate")
            End If
            System.Diagnostics.Debug.WriteLine(num.ToString)
        Next
        randomizeCards(rand)

        For i As Integer = 0 To 15
            cardArray(i).button.BackColor = Color.DarkTurquoise
            cardArray(i).button.Text = "X"
        Next
    End Sub

    Sub cardSelection(ByVal cardnum As Integer)
        If cardArray(cardnum).button.BackColor = Color.DarkTurquoise Then
            score += 1
            lblScore.Text = "Score: " & score.ToString
            selectionCount += 1
            cardArray(cardnum).button.BackColor = Color.LimeGreen
            cardArray(cardnum).button.Text = cardArray(cardnum).value.ToString

            If (selectionCount = 2) Then
                Button1.Enabled = False
                Button2.Enabled = False
                Button3.Enabled = False
                Button4.Enabled = False
                Button5.Enabled = False
                Button6.Enabled = False
                Button7.Enabled = False
                Button8.Enabled = False
                Button9.Enabled = False
                Button10.Enabled = False
                Button11.Enabled = False
                Button12.Enabled = False
                Button13.Enabled = False
                Button14.Enabled = False
                Button15.Enabled = False
                Button16.Enabled = False

                activeCardTwo = cardArray(cardnum)
                If (activeCardOne.button.Text = activeCardTwo.button.Text) Then
                    numMatches += 1
                    MessageBox.Show("You got a Match!")
                    activeCardOne.button.BackColor = Color.Gray
                    activeCardTwo.button.BackColor = Color.Gray

                    activeCardOne = Nothing
                    activeCardTwo = Nothing
                End If
                selectionCount = 0
                timer.Enabled = True
                If (numMatches = 8) Then
                    updateScoresArrayFromFile()
                    Dim tenthHeighest As Integer
                    Integer.TryParse(hScores(9).Substring(hScores(9).Length - 2, 2), tenthHeighest)
                    If score < tenthHeighest Then
                        MessageBox.Show("Congratulations! you placed in the top ten with a score of " & score.ToString)
                        hScores(9) = score.ToString
                        record()
                    Else
                        MessageBox.Show("Nice Job! you finished with a score of " & score.ToString)
                    End If
                    initializeGame()
                End If
            Else
                activeCardOne = cardArray(cardnum)
            End If

        End If
    End Sub

    Sub randomizeCards(rand As Random)
        For cardIndex As Integer = 0 To 15
            Dim reset As Boolean = False
            Dim rCol As Integer = rand.Next(0, 7)
            Dim rRow As Integer = rand.Next(0, 2)

            System.Diagnostics.Debug.WriteLine(cardIndex.ToString)

            cardArray(cardIndex).value = randomArray(rRow, rCol)

            Do Until cardArray(cardIndex).value <> 0
                If rCol = 7 Then
                    cardIndex -= 1
                    reset = True
                    Exit Do
                End If
                rCol += 1
                cardArray(cardIndex).value = randomArray(rRow, rCol)
                'System.Diagnostics.Debug.WriteLine("untilloop")
            Loop

            If reset <> True Then
                randomArray(rRow, rCol) = 0
            Else
                System.Diagnostics.Debug.WriteLine("reset")
            End If

        Next
    End Sub

    Sub record()
        Dim outFile As IO.StreamWriter = IO.File.CreateText("D:\Carlos\My Documents\CoS\COMP9\Project 1\WindowsApp1\WindowsApp1\HighScores.txt")
        For i As Integer = 9 To 0 Step -1
            outFile.WriteLine(hScores(i))
        Next
        outFile.Close()
    End Sub

    Sub updateScoresArrayFromFile()
        Dim inFile As IO.StreamReader
        If IO.File.Exists("D:\Carlos\My Documents\CoS\COMP9\Project 1\WindowsApp1\WindowsApp1\HighScores.txt") Then
            inFile = IO.File.OpenText("D:\Carlos\My Documents\CoS\COMP9\Project 1\WindowsApp1\WindowsApp1\HighScores.txt")

            For i As Integer = 0 To 9
                hScores(i) = inFile.ReadLine()
            Next
            inFile.Close()
            Array.Sort(hScores)
        Else
            MessageBox.Show("High Scores File Not Found")
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        cardSelection(0)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        cardSelection(1)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        cardSelection(2)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        cardSelection(3)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        cardSelection(4)
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        cardSelection(5)
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        cardSelection(6)
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        cardSelection(7)
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        cardSelection(8)
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        cardSelection(9)
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        cardSelection(10)
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        cardSelection(11)
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        cardSelection(12)
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        cardSelection(13)
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        cardSelection(14)
    End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        cardSelection(15)
    End Sub

    Private Sub timer_Tick(sender As Object, e As EventArgs) Handles timer.Tick

        timer.Enabled = False
        For i As Integer = 0 To 15
            If cardArray(i).button.BackColor = Color.LimeGreen Then
                cardArray(i).button.BackColor = Color.DarkTurquoise
                cardArray(i).button.Text = "X"
            End If
        Next
        Button1.Enabled = True
        Button2.Enabled = True
        Button3.Enabled = True
        Button4.Enabled = True
        Button5.Enabled = True
        Button6.Enabled = True
        Button7.Enabled = True
        Button8.Enabled = True
        Button9.Enabled = True
        Button10.Enabled = True
        Button11.Enabled = True
        Button12.Enabled = True
        Button13.Enabled = True
        Button14.Enabled = True
        Button15.Enabled = True
        Button16.Enabled = True
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        initializeGame()
    End Sub

    Private Sub btnScores_Click(sender As Object, e As EventArgs) Handles btnScores.Click

        Dim scoresText As String = ""

        updateScoresArrayFromFile()
        For j As Integer = 0 To 9
            scoresText += hScores(j) & ControlChars.NewLine
        Next

        MessageBox.Show(scoresText)
    End Sub
End Class
