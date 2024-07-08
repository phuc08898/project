export interface IConfigDto
{
    key: string;
    value: string;
    desc: string | null;
}

export interface IConfigUpdateArg
{
    value: string;
    desc: string | null;
}

