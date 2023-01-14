//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MusicMediaPlayer.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Song
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Song()
        {
            this.PlayLists = new HashSet<PlayList>();
        }
    
        public int SongId { get; set; }
        public string SongTitle { get; set; }
        public Nullable<int> Times { get; set; }
        public string Artist { get; set; }
        public string FilePath { get; set; }
        public Nullable<bool> IsFavourite { get; set; }
        public string HowLong { get; set; }
        public Nullable<System.DateTime> TimeAdd { get; set; }
        public byte[] ImageSongBinary { get; set; }
        public Nullable<int> UserId { get; set; }
        public string Genre { get; set; }
        public string Album { get; set; }
        public Nullable<int> ArtistId { get; set; }
        public Nullable<int> AlbumId { get; set; }
        public Nullable<int> GenreId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PlayList> PlayLists { get; set; }
        public virtual UserAccount UserAccount { get; set; }
        public virtual Album Album1 { get; set; }
        public virtual Artist Artist1 { get; set; }
        public virtual Genre Genre1 { get; set; }
    }
}
