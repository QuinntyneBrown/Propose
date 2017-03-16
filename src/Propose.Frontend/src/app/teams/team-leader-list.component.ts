import { TeamLeader } from "./team-leader.model";
import { TeamLeaderService } from "./team-leader.service";

const template = require("./team-leader-list.component.html");
const styles = require("./team-leader-list.component.scss");

export class TeamLeaderListComponent extends HTMLElement {
    constructor(
        private _document: Document = document,
        private _teamLeaderService: TeamLeaderService = TeamLeaderService.Instance) {
        super();
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
    }

    private async _bind() {
        const teamLeaders: Array<TeamLeader> = await this._teamLeaderService.get();
        for (var i = 0; i < teamLeaders.length; i++) {
            let el = this._document.createElement(`ce-team-leader-item`);
            el.setAttribute("entity", JSON.stringify(teamLeaders[i]));
            this.appendChild(el);
        }    
    }
}

customElements.define("ce-team-leader-list", TeamLeaderListComponent);
