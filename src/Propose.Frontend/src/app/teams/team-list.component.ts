import { Team } from "./team.model";
import { TeamService } from "./team.service";

const template = require("./team-list.component.html");
const styles = require("./team-list.component.scss");

export class TeamListComponent extends HTMLElement {
    constructor(
        private _document: Document = document,
        private _teamService: TeamService = TeamService.Instance) {
        super();
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
    }

    private async _bind() {
        const teams: Array<Team> = await this._teamService.get();
        for (var i = 0; i < teams.length; i++) {
            let el = this._document.createElement(`ce-team-item`);
            el.setAttribute("entity", JSON.stringify(teams[i]));
            this.appendChild(el);
        }    
    }
}

customElements.define("ce-team-list", TeamListComponent);
