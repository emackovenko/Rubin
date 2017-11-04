<?php
/**
 * Автор: Евгений Маковенко
 * Дата: 23.06.2017
 * Описание: Вывод встраиваемой страницы с фильтруемым списком абитуриентов
 */

// Параметры подключения к БД
error_reporting(0);
$db_host = 'DATABASE_SERVER_ADDRESS';
$db_user = 'DATABASE_USERNAME';
$db_password = 'DATABASE_USER_PASSWORD';
$db_name = 'DATABASE_NAME';

$link = new MySQLi($db_host, $db_user, $db_password, $db_name);
if ($link->connect_error) {
    die('<p style="color:red">'.$link->connect_errno.' - '.$link->connect_error.'</p>');
}
$link->query("SET NAMES utf8");

// обработка формы фильтрации

if (isset($_POST['educationForm']))
{

    $educationForm = $_POST['educationForm'];
}
else
{
    $educationForm = 1;
}

if (isset($_POST['direction']))
{
    $direction = $_POST['direction'];
}
else
{
    $direction = 1;
}

if (isset($_POST['category']))
{
    $category = $_POST['category'];
}
else
{
    $category = 1;
}

if (isset($_POST['onlyOriginals']))
{
    $onlyOriginals = ($_POST['onlyOriginals'] == "true") ? true : false;
}
else
{
    $onlyOriginals = false;
}

if (isset($_POST['onlyFirstPriority']))
{
    $onlyFirstPriority = ($_POST['onlyFirstPriority'] == "true") ? true : false;
}
else
{
    $onlyFirstPriority = false;
}


//раскидываем категории
if ($category == 1)
{
    $qCategory = '1, 2, 3';
}
elseif ($category == 2)
{
    $qCategory = '1, 3';
}
else
{
    $qCategory = '2';
}


// получить количество мест для приёма по направлению и форме обучения
$placesBudget = 0;
$placesQuota = 0;
$placesPaid = 0;

$queryString =
    "SELECT 
    cg.FinanceSourceId AS FinanceSourceId,
    cg.PlaceCount AS Places
    FROM CompetitiveGroups cg 
    WHERE cg.FinanceSourceId IN ({$qCategory}) AND cg.EducationFormId = {$educationForm} AND cg.DirectionId = {$direction};";
$result = $link->query($queryString);

// раскидываем результат
while ($row = $result->fetch_assoc())
{
    if ($row['FinanceSourceId'] == 1)
    {
        $placesBudget = $row['Places'];
    }
    elseif ($row['FinanceSourceId'] == 2)
    {
        $placesPaid = $row['Places'];
    }
    elseif ($row['FinanceSourceId'] == 3)
    {
        $placesQuota = $row['Places'];
    }
}

?>

<!-- Количество мест для приёма -->
<div
    id="entrant_content">
    <p>Количество мест для приёма на места, финансирумые за счёт средств федерального бюджета по общему конкурсу: <?echo $placesBudget?>. <br>
        В том числе по конкурсу в рамках особой квоты: <?echo $placesQuota?>.<br>
        Количество мест для приёма на места по договорам об оказании платных образовательных услуг: <?echo $placesPaid?>.</p>
</div>

<!-- Таблица абитуриентов -->
<div
    id="entrant_content">
    <table
        id="entrnat_list">
        <tr>
            <th>№</th>
            <th>Ф.И.О.</th>
            <th>Регистрационный номер</th>
            <th>Документ</th>
            <th>Категория приёма</th>
            <th>Приоритет</th>
            <th>Сумма баллов</th>
        </tr>
        <?php

        // получаем список абитуриентов

        // формируем запрос из трех составных частей: тело запроса, условие и соритровка
        $queryStringBody = "
SELECT
	CONCAT_WS(' ', e.LastName, e.FirstName, e.Patronymic) AS EntrantName,
	c.Number AS RegistrationNumber,
	IF(IsOriginal(c.Id), 'оригинал', 'копия') AS OriginalOrCopy,
	fs.Name AS Category,
	cc.Priority AS Priority,
	GetEtrantTestMark(c.Id) AS TestMark
	FROM Claims c
	INNER JOIN Entrants e ON c.Id = e.ClaimId
	INNER JOIN ClaimConditions cc ON c.Id = cc.ClaimId
	INNER JOIN CompetitiveGroups cg ON cc.CompetitiveGroupId = cg.Id
	INNER JOIN EducationForms ef ON cg.EducationFormId = ef.Id
	INNER JOIN FinanceSources fs ON cg.FinanceSourceId = fs.Id
	INNER JOIN Directions d ON cg.DirectionId = d.Id ";

        $queryStringFilter = "WHERE ef.Id = {$educationForm} AND d.Id = {$direction} AND fs.Id IN ({$qCategory}) ";
        if ($onlyOriginals)
        {
            $queryStringFilter .= "AND IsOriginal(c.Id) ";
        }
        if ($onlyFirstPriority)
        {
            $queryStringFilter .= "AND cc.Priority = 1 ";
        }

        $queryStringSortOrder = "ORDER BY fs.SortNumber ASC, TestMark DESC, c.RegistrationDate, EntrantName;";

        $queryString = $queryStringBody . $queryStringFilter . $queryStringSortOrder;

        $result = $link->query($queryString);

        $i = 1;

        while ($row = $result->fetch_assoc())
        {
            if ($row['TestMark'] <= 10)
            {
                $testMark = "будет сдавать экзамены";
            }
            else
            {
                $testMark = $row['TestMark'];
            }

            echo '<tr>';
            echo '<td align="center">' . $i . '</td>';
            echo '<td align="left">' . $row['EntrantName'] . '</td>';
            echo '<td align="center">' . $row['RegistrationNumber'] . '</td>';
            echo '<td align="center">' . $row['OriginalOrCopy'] . '</td>';
            echo '<td align="center">' . $row['Category'] . '</td>';
            echo '<td align="center">' . $row['Priority'] . '</td>';
            echo '<td align="center">' . $testMark . '</td>';
            echo '</tr>';
            $i++;
        }

        ?>
    </table>
</div>
