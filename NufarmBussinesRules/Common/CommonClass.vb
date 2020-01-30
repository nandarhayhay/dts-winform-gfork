Namespace common
    Public Class CommonClass
        Public Shared Function getNumericFromDate(ByVal dateobject As DateTime) As String
            Dim strDecDate = ""
            Dim strYear As String = CStr(Year(dateobject)), strMonth As String = CStr(Month(dateobject))
            If strMonth.Length < 2 Then : strMonth = strMonth.Insert(0, "0") : End If
            Dim strDay As String = CStr(Day(dateobject))
            If strDay.Length < 2 Then : strDay = strDay.Insert(0, "0") : End If
            strDecDate = strYear + "" + strMonth + "" + strDay
            Return strDecDate
        End Function
        Public Shared Function ResolveCriteria(ByVal criteria As Helper.CriteriaSearch, ByVal dataType As Helper.DataTypes, _
        ByVal value As Object) As String
            Dim Query As String = "", myDateTime As String = ""
            Select Case criteria
                Case Helper.CriteriaSearch.BeginWith
                    Select Case dataType
                        Case Helper.DataTypes.NVarChar, Helper.DataTypes.Text, Helper.DataTypes.VarChar, Helper.DataTypes.NText, Helper.DataTypes.Char, Helper.DataTypes.NChar
                            Query = String.Format(" LIKE '{0}%'", value)
                        Case Else
                            Throw New Exception("Operator is not supported for this kind of datatype")
                    End Select
                Case Helper.CriteriaSearch.EndWith
                    Select Case dataType
                        Case Helper.DataTypes.NVarChar, Helper.DataTypes.Text, Helper.DataTypes.VarChar, Helper.DataTypes.NText, Helper.DataTypes.Char, Helper.DataTypes.NChar
                            Query = String.Format(" LIKE '%{0}'", value)
                        Case Else
                            Throw New Exception("Operator is not supported for this kind of datatype")
                    End Select
                Case Helper.CriteriaSearch.Equal
                    Select Case dataType
                        Case Helper.DataTypes.NVarChar, Helper.DataTypes.Text, Helper.DataTypes.VarChar, Helper.DataTypes.NText, Helper.DataTypes.Char, Helper.DataTypes.NChar
                            Query = String.Format(" = '{0}'", value)
                        Case Helper.DataTypes.Integer, Helper.DataTypes.Decimal, Helper.DataTypes.Float, Helper.DataTypes.BigInt
                            Query = String.Format(" = {0}", value)
                        Case Helper.DataTypes.DateTime
                            Dim tempDate As DateTime
                            If (Not DateTime.TryParse(value.ToString(), tempDate)) Then
                                Throw New Exception("Invalid format value for this kind of datatype")
                            End If
                            myDateTime = tempDate.Month.ToString() + "/" + tempDate.Day.ToString() + "/" + tempDate.Year.ToString()
                            Query = String.Format(" = '{0}'", myDateTime)
                        Case Else
                            Throw New Exception("Operator is not supported for this kind of datatype")
                    End Select
                Case Helper.CriteriaSearch.Greater
                    Select Case dataType
                        Case Helper.DataTypes.BigInt, Helper.DataTypes.Decimal, Helper.DataTypes.Float, Helper.DataTypes.Integer
                            Query = String.Format(" > {0}", value)
                        Case Helper.DataTypes.DateTime
                            Dim tempDate As DateTime
                            If (Not DateTime.TryParse(value.ToString(), tempDate)) Then
                                Throw New Exception("Invalid format value for this kind of datatype")
                            End If
                            myDateTime = tempDate.Month.ToString() + "/" + tempDate.Day.ToString() + "/" + tempDate.Year.ToString()
                            Query = String.Format(" > '{0}'", myDateTime)
                        Case Else
                            Throw New Exception("Operator is not supported for this kind of datatype")
                    End Select
                Case Helper.CriteriaSearch.GreaterOrEqual
                    Select Case dataType
                        Case Helper.DataTypes.BigInt, Helper.DataTypes.Decimal, Helper.DataTypes.Float, Helper.DataTypes.Integer
                            Query = String.Format(" >= {0}", value)
                        Case Helper.DataTypes.DateTime
                            Dim tempDate As DateTime
                            If (Not DateTime.TryParse(value.ToString(), tempDate)) Then
                                Throw New Exception("Invalid format value for this kind of datatype")
                            End If
                            myDateTime = tempDate.Month.ToString() + "/" + tempDate.Day.ToString() + "/" + tempDate.Year.ToString()
                            Query = String.Format(" >= '{0}'", myDateTime)
                        Case Else
                            Throw New Exception("Operator is not supported for this kind of datatype")
                    End Select
                Case Helper.CriteriaSearch.In
                    Select Case dataType
                        Case Helper.DataTypes.NVarChar, Helper.DataTypes.Text, Helper.DataTypes.VarChar, Helper.DataTypes.NText, Helper.DataTypes.Char, Helper.DataTypes.NChar
                            Query = String.Format(" IN ('{0}')", value)
                        Case Helper.DataTypes.DateTime
                            Dim tempDate As DateTime
                            If (Not DateTime.TryParse(value.ToString(), tempDate)) Then
                                Throw New Exception("Invalid format value for this kind of datatype")
                            End If
                            myDateTime = tempDate.Month.ToString() + "/" + tempDate.Day.ToString() + "/" + tempDate.Year.ToString()
                            Query = String.Format(" IN '{0}'", tempDate)
                        Case Helper.DataTypes.BigInt, Helper.DataTypes.Integer, Helper.DataTypes.Decimal, Helper.DataTypes.Float
                            Query = String.Format(" IN ({0})", value)
                        Case Else
                            Throw New Exception("Operator is not supported for this kind of datatype")
                    End Select
                Case Helper.CriteriaSearch.Less
                    Select Case dataType
                        Case Helper.DataTypes.BigInt, Helper.DataTypes.Integer, Helper.DataTypes.Decimal, Helper.DataTypes.Float
                            Query = String.Format(" < {0}", value)
                        Case Helper.DataTypes.DateTime
                            Dim tempDate As DateTime
                            If (Not DateTime.TryParse(value.ToString(), tempDate)) Then
                                Throw New Exception("Invalid format value for this kind of datatype")
                            End If
                            myDateTime = tempDate.Month.ToString() + "/" + tempDate.Day.ToString() + "/" + tempDate.Year.ToString()
                            Query = String.Format(" < '{0}'", myDateTime)
                        Case Else
                            Throw New Exception("Operator is not supported for this kind of datatype")
                    End Select
                Case Helper.CriteriaSearch.LessOrEqual
                    Select Case dataType
                        Case Helper.DataTypes.BigInt, Helper.DataTypes.Integer, Helper.DataTypes.Decimal, Helper.DataTypes.Float
                            Query = String.Format(" <= {0}", value)
                        Case Helper.DataTypes.DateTime
                            Dim tempDate As DateTime
                            If (Not DateTime.TryParse(value.ToString(), tempDate)) Then
                                Throw New Exception("Invalid format value for this kind of datatype")
                            End If
                            myDateTime = tempDate.Month.ToString() + "/" + tempDate.Day.ToString() + "/" + tempDate.Year.ToString()
                            Query = String.Format(" <= '{0}'", myDateTime)
                        Case Else
                            Throw New Exception("Operator is not supported for this kind of datatype")
                    End Select
                Case Helper.CriteriaSearch.Like
                    Select Case dataType
                        Case Helper.DataTypes.NVarChar, Helper.DataTypes.Text, Helper.DataTypes.VarChar, Helper.DataTypes.NText, Helper.DataTypes.Char, Helper.DataTypes.NChar
                            Query = String.Format(" LIKE '%{0}%'", value)
                        Case Else
                            Throw New Exception("Operator is not supported for this kind of datatype")

                    End Select
                Case Helper.CriteriaSearch.NotEqual
                    Select Case dataType
                        Case Helper.DataTypes.NVarChar, Helper.DataTypes.Text, Helper.DataTypes.VarChar, Helper.DataTypes.NText, Helper.DataTypes.Char, Helper.DataTypes.NChar
                            Query = String.Format(" != '{0}'", value)
                        Case Helper.DataTypes.DateTime
                            Dim tempDate As DateTime
                            If (Not DateTime.TryParse(value.ToString(), tempDate)) Then
                                Throw New Exception("Invalid format value for this kind of datatype")
                            End If
                            myDateTime = tempDate.Month.ToString() + "/" + tempDate.Day.ToString() + "/" + tempDate.Year.ToString()
                            Query = String.Format(" <> '{0}'", myDateTime)
                        Case Else
                            Query = String.Format(" <> {0}", value)
                    End Select
            End Select
            Return Query
        End Function

        Public Shared Function GetPercentage(ByVal persen As Decimal, ByVal SUM_PO_ORIGINALQTY As Decimal, ByVal TARGET_QTY As Decimal) As Decimal
            If TARGET_QTY <= 0 Then
                Return 0
            End If
            Return (SUM_PO_ORIGINALQTY * persen * 1) / TARGET_QTY
            'Return IIf((TARGET_QTY <= 0), 0, ((SUM_PO_ORIGINALQTY * persen * 1) / TARGET_QTY))
        End Function
        Public Shared Function getCodeGroupDistributor(ByVal DistributorNames As String) As String

            '    Case GroupDistributor.BIJ
            'Return "BIJ"
            '    Case GroupDistributor.PANCA
            'Return "PAN"
            '    Case GroupDistributor.SANTANI
            'Return "SAN"
            'End Select
            If DistributorNames.Contains("PANCA AGRO") Then
                Return "PAN"
            ElseIf DistributorNames.Contains("SANTANI") Or DistributorNames.Contains("TANI SEJAHTERA") Then
                Return "SAN"
            ElseIf DistributorNames.Contains("BUMI INTAN JAYA") Then
                Return "BIJ"
            End If
            Return ""
        End Function
        
    End Class

    Public Class dataTManager
        Public SearchBy As String = "", SearchValue As Object = Nothing, MaxRecord As Integer = 0, CriteriaSearch As String = ""

    End Class

    Public Class Helper
        Public Enum CriteriaSearch
            Equal
            BeginWith
            EndWith
            NotEqual
            Greater
            Less
            LessOrEqual
            GreaterOrEqual
            [Like]
            [In]
            NotIn
        End Enum
        Public Enum DataTypes
            BigInt
            [Boolean]
            [Char]
            DateTime
            [Decimal]
            Float
            Image
            [Integer]
            Money
            NChar
            NText
            NVarChar
            Real
            UniqueIdentifier
            [Short]
            Text
            [Byte]
            VarChar
            [Variant]
        End Enum
        Public Enum SaveMode
            Insert
            Update
            None
        End Enum
    End Class
End Namespace
