package team.incorpore.dr0gis.elqueue.main;

import android.content.Intent;
import android.support.v7.widget.AppCompatButton;
import android.support.v7.widget.RecyclerView;
import android.view.View;
import android.widget.ImageView;
import android.widget.TextView;

import team.incorpore.dr0gis.elqueue.App;
import team.incorpore.dr0gis.elqueue.R;
import team.incorpore.dr0gis.elqueue.server.queue.EventResult;
import team.incorpore.dr0gis.elqueue.server.queue.QueueResult;
import team.incorpore.dr0gis.elqueue.users.QueueUserList;

/**
 * Created by dr0gi on 06.12.2017.
 */

public class QueueHolder extends RecyclerView.ViewHolder {
    private View itemView;
    private ImageView ivTypeIcon;
    private TextView tvName;
    private TextView tvEvent;
    private TextView tvOrganization;
    private TextView tvDescription;

    QueueHolder(View itemView) {
        super(itemView);

        this.itemView = itemView;
        ivTypeIcon = itemView.findViewById(R.id.ivTypeIcon);
        tvName = itemView.findViewById(R.id.tvName);
        tvEvent = itemView.findViewById(R.id.tvEvent);
        tvOrganization = itemView.findViewById(R.id.tvOrganization);
        tvDescription = itemView.findViewById(R.id.tvDescription);
    }

    void bindData(QueueResult queue, MainActivity.OpenUsersList listener) {
        itemView.setOnClickListener(listener);

        ivTypeIcon.setImageResource(EventResult.Types.get(queue.getEvent().getType()));
        tvName.setText(queue.getName());
        tvEvent.setText(queue.getEvent().getName());
        tvOrganization.setText(queue.getEvent().getOrganization().getName());
        tvDescription.setText(queue.getDescription());
    }
}
