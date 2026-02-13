
const { Database } = require("sqlite3");
/**
 * run(sql, [params], [callback]) — для команд, которые не возвращают данные (INSERT, UPDATE, DELETE, CREATE TABLE).
    get(sql, [params], [callback]) — возвращает только первую строку результата (удобно для поиска по ID).
    all(sql, [params], [callback]) — возвращает массив всех найденных строк.
    each(sql, [params], [callback], [complete]) — выполняет колбэк для каждой строки по отдельности (полезно при больших объемах данных).
    exec(sql, [callback]) — выполняет сразу несколько SQL-команд, разделенных точкой с запятой (без поддержки параметров).
    prepare(sql, [params], [callback]) — создает объект Statement для многократного выполнения одного и того же запроса.
    close([callback]) — закрывает соединение с базой данных.
    configure(option, value) — настройка поведения (например, обработка исключений). 
 */
/** 
 * @param {Database} dataBase
 */
function initDatabase(dataBase) {
    dataBase.exec(`INSERT OR IGNORE INTO ${global.dbTables.tool} (id, name) values(1, 'Home');
        INSERT OR IGNORE INTO ${global.dbTables.tool} (id, name) values(2, 'Finances');`);
}

module.exports = { initDatabase: initDatabase };