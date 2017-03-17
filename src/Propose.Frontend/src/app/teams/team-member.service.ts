import { fetch } from "../utilities";
import { TeamMember } from "./team-member.model";

export class TeamMemberService {
    constructor(private _fetch = fetch) { }

    private static _instance: TeamMemberService;

    public static get Instance() {
        this._instance = this._instance || new TeamMemberService();
        return this._instance;
    }

    public get(): Promise<Array<TeamMember>> {
        return this._fetch({ url: "/api/teammember/get", authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { teamMembers: Array<TeamMember> }).teamMembers;
        });
    }

    public getById(id): Promise<TeamMember> {
        return this._fetch({ url: `/api/teammember/getbyid?id=${id}`, authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { teamMember: TeamMember }).teamMember;
        });
    }

    public add(teamMember) {
        return this._fetch({ url: `/api/teammember/add`, method: "POST", data: { teamMember }, authRequired: true  });
    }

    public remove(options: { id : number }) {
        return this._fetch({ url: `/api/teammember/remove?id=${options.id}`, method: "DELETE", authRequired: true  });
    }
    
}
