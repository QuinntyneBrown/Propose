import { fetch } from "../utilities";
import { Team } from "./team.model";

export class TeamService {
    constructor(private _fetch = fetch) { }

    private static _instance: TeamService;

    public static get Instance() {
        this._instance = this._instance || new TeamService();
        return this._instance;
    }

    public get(): Promise<Array<Team>> {
        return this._fetch({ url: "/api/team/get", authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { teams: Array<Team> }).teams;
        });
    }

    public getById(id): Promise<Team> {
        return this._fetch({ url: `/api/team/getbyid?id=${id}`, authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { team: Team }).team;
        });
    }

    public add(team) {
        return this._fetch({ url: `/api/team/add`, method: "POST", data: { team }, authRequired: true  });
    }

    public remove(options: { id : number }) {
        return this._fetch({ url: `/api/team/remove?id=${options.id}`, method: "DELETE", authRequired: true  });
    }
    
}
