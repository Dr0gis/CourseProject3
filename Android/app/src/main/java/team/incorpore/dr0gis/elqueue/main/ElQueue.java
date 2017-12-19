package team.incorpore.dr0gis.elqueue.main;

import team.incorpore.dr0gis.elqueue.R;

/**
 * Created by dr0gi on 06.12.2017.
 */

public class ElQueue {
    private String name;
    private String event;
    private String organization;
    private String description;
    private Type type;

    enum Type {
        FOOTBALL(R.drawable.football),
        CONCERT(R.drawable.concert),
        CYBERSPORT(R.drawable.cybersport),
        CONFERENCE(R.drawable.conference);

        private final int id;

        Type(int id) {
            this.id = id;
        }

        public int getId() {
            return id;
        }
    }

    public ElQueue(String name, String event, String organization, String description, Type type) {
        this.name = name;
        this.event = event;
        this.organization = organization;
        this.description = description;
        this.type = type;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getEvent() {
        return event;
    }

    public void setEvent(String event) {
        this.event = event;
    }

    public String getOrganization() {
        return organization;
    }

    public void setOrganization(String organization) {
        this.organization = organization;
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public Type getType() {
        return type;
    }

    public void setType(Type type) {
        this.type = type;
    }
}
