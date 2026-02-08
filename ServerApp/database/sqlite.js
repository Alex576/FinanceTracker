const sqlite3 = require('sqlite3').verbose();
const dbScripts = require('./scripts/db-scripts');
// Файл 'my_database.db' создастся в корне проекта
const db = new sqlite3.Database('./my_database.db', (err) => {
    if (err) console.error('Ошибка подключения:', err.message);
    else console.log('База данных SQLite создана/подключена.');
});
global.dbTables = {
    finances: 'finances',
    tool: 'tool',
    layout: 'layout',
};
// Создаем таблицу при запуске, если её еще нет
db.serialize(() => {
    db.run(`CREATE TABLE IF NOT EXISTS ${global.dbTables.finances} (
        id INTEGER PRIMARY KEY,
        name TEXT NOT NULL,
        optionJson TEXT
        )`);
    db.run(`CREATE TABLE IF NOT EXISTS ${global.dbTables.tool} (
        id INTEGER PRIMARY KEY,
        name TEXT NOT NULL
        )`);
    db.run(`CREATE TABLE IF NOT EXISTS ${global.dbTables.layout} (
        id INTEGER PRIMARY KEY AUTOINCREMENT,
        toolCode INTEGER NOT NULL,
        layoutJson TEXT,
        FOREIGN KEY (toolCode) REFERENCES tool (id)
        )`);
    dbScripts.initDatabase(db);
});
module.exports = db;