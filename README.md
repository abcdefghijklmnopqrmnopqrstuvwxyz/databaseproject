# Database Management

This is school project for managing database. Code is fully written in *C#*.

Project Functions:
```
1. Insert into database
2. Update database
3. Delete from database
4. Import data into database
```

*Theres one more function that is not yet implemented - which is data export*

### Insert Data
To insert data, you needs to fill **all required textboxes/numericupdowns**, otherwise error will appear.

### Update Data
To update some data, you needs to write new values for selected ID.
You can select ID by putting its value into numericupdown box.

### Delete Data
To delete data, you just simply put ID into numericupdown and if theres ID that matches, it will be deleted.

### Import Data
To import data, you needs to have **active tab, that matched table** you wants to import data into. Data can be imported inly in *XML* format and must match database table columns.

### Error Handling
Program is stable and should react to every error like invalid foreign key, null values etc.
So the program should not crash, basically it is `#1589F0`"shyshkaproof"

#### Run Program
To run the program, you can go to "/bin/Debug/WindowsFormsApp1.exe" or just simply execute Database Management run.bat in the default folder.
