import { TeamMember } from "./team-member.model";
import { TeamMemberService } from "./team-member.service";

const template = require("./team-member-list.component.html");
const styles = require("./team-member-list.component.scss");

export class TeamMemberListComponent extends HTMLElement {
    constructor(
        private _document: Document = document,
        private _teamMemberService: TeamMemberService = TeamMemberService.Instance) {
        super();
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
    }

    private async _bind() {
        const teamMembers: Array<TeamMember> = await this._teamMemberService.get();
        for (var i = 0; i < teamMembers.length; i++) {
            let el = this._document.createElement(`ce-team-member-item`);
            el.setAttribute("entity", JSON.stringify(teamMembers[i]));
            this.appendChild(el);
        }    
    }
}

customElements.define("ce-team-member-list", TeamMemberListComponent);
