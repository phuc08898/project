import { IConfigDto, IConfigUpdateArg } from "@/interfaces";
const { ipcRenderer } = window.require('electron');

export class ConfigService { 
    public async GetAllAsync() {
        return await ipcRenderer.invoke("database.queryAsync",
            `SELECT * FROM Configs;`
        );
    }

    public async InsertAsync(args: IConfigDto[]) { 
        let stringTemplate = "INSERT INTO Configs (`key`, `value`, `desc`) VALUES ";

        args.map((arg) => {
            stringTemplate += `("${arg.key}", "${arg.value}", "${arg.desc}"),`
        })

        stringTemplate = stringTemplate.replace(/,$/, ';');
        
        const affectedRows = await ipcRenderer.invoke("database.executeAsync",
            stringTemplate
        );

        return affectedRows;
    }

    public async DeleteAsync(key: string) { 
        let stringTemplate = `DELETE FROM Configs WHERE \`key\` = "${key}" `;
 
        const affectedRows = await ipcRenderer.invoke("database.executeAsync",
            stringTemplate
        );

        return affectedRows;
    }

    public async UpdateAsync(key: string, data: IConfigUpdateArg) { 
        let stringTemplate = `UPDATE Configs SET 
            \`value\` = "${data.value}", 
            \`desc\` = "${data.desc}" 
            WHERE \`key\` = "${key}"`;
    
        const affectedRows = await ipcRenderer.invoke("database.executeAsync",
            stringTemplate
        );

        return affectedRows;
    }
}