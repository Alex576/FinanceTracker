
const { Database } = require("sqlite3");

/** 
 * @param {Database} dataBase
 */
function initDatabase(dataBase) {
    dataBase.run(`INSERT OR IGNORE INTO ${global.dbTables.tool} (id, name) values(1, 'Finances')`);
}

module.exports = { initDatabase: initDatabase };