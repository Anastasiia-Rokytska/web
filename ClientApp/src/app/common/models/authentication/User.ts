import { Group } from "../general/Group";
import { RoleViewModel } from "../general/RoleViewModel";
import { Role } from "./role";

export interface User {
    userId: number;
    name: string;
    surname: string;
    userName: string;
    roles: RoleViewModel[];
    accessToken?: string;
    group: Group;
    password: string;
}

