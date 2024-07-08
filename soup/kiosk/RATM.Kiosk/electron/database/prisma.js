import { PrismaClient } from "@prisma/client";

export class DatabaseContext {
    constructor(){
        this.prisma = null;
    }
    GetInstance() {
        if (!this.prisma) {
            this.prisma = new PrismaClient({
                datasources: {
                    db: {
                        // TODO: replace with .env file
                        url: "mysql://root:mauFJcuf5dhRMQrjj@localhost:3306/ratm",
                        // options: {
                        //     connectionParameters: {
                        //         max: 20,
                        //         idleTimeoutMillis: 10000,
                        //         connectionTimeoutMillis: 10000
                        //     }
                        // }
                    }
                },
                log: process.env.NODE_ENV === 'dev' ? ['query'] : []
            });
        }
        return this.prisma;
    }
}