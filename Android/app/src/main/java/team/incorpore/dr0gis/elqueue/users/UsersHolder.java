package team.incorpore.dr0gis.elqueue.users;

import android.support.v7.widget.RecyclerView;
import android.view.View;
import android.widget.ImageView;
import android.widget.TextView;

import team.incorpore.dr0gis.elqueue.R;
import team.incorpore.dr0gis.elqueue.server.queue.QueueResult;
import team.incorpore.dr0gis.elqueue.server.users.UsersResult;

/**
 * Created by dr0gi on 19.12.2017.
 */

public class UsersHolder extends RecyclerView.ViewHolder  {
    private TextView tvNumber;
    private TextView tvEmail;
    private TextView tvTime;

    UsersHolder(View itemView) {
        super(itemView);

        tvNumber = itemView.findViewById(R.id.tvNumber);
        tvEmail = itemView.findViewById(R.id.tvEmail);
        tvTime = itemView.findViewById(R.id.tvTime);
    }

    void bindData(UsersResult user, int position) {
        tvNumber.setText(1 + position + "");
        tvEmail.setText(user.getEmail());
        tvTime.setText(user.getDateTimeRegistration());
    }
}
