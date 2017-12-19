package team.incorpore.dr0gis.elqueue.users;

import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import java.util.List;

import team.incorpore.dr0gis.elqueue.R;
import team.incorpore.dr0gis.elqueue.server.users.UsersResult;

/**
 * Created by dr0gi on 19.12.2017.
 */

public class UsersAdapter extends RecyclerView.Adapter<UsersHolder> {
    private List<UsersResult> usersResultList;

    UsersAdapter(List<UsersResult> usersResultList) {
        this.usersResultList = usersResultList;
    }

    @Override
    public UsersHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        LayoutInflater li = LayoutInflater.from(parent.getContext());
        View view = li.inflate(R.layout.item_queue_user, parent, false);

        return new UsersHolder(view);
    }

    @Override
    public void onBindViewHolder(UsersHolder holder, int position) {
        holder.bindData(usersResultList.get(position), position);
    }

    @Override
    public int getItemCount() {
        return usersResultList.size();
    }
}
